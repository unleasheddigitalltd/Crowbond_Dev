using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Outbox;
using Crowbond.Common.Infrastructure.Serialization;
using Crowbond.Modules.OMS.Application;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;
using System.Data;
using System.Data.Common;

namespace Crowbond.Modules.OMS.Infrastructure.Outbox;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxJob(
    IDbConnectionFactory dbConnectionFactory,
    IServiceScopeFactory serviceScopeFactory,
    IDateTimeProvider dateTimeProvider,
    IOptions<OutboxOptions> outboxOptions,
    ILogger<ProcessOutboxJob> logger) : IJob
{
    private const string ModuleName = "oms";

    public async Task Execute(IJobExecutionContext context)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        await using DbTransaction transaction = await connection.BeginTransactionAsync();

        IReadOnlyList<OutboxMessageResponse> outboxMessages = await GetOutboxMessagesAsync(connection, transaction);

        foreach (OutboxMessageResponse outboxMessage in outboxMessages)
        {
            Exception? exception = null;
            try
            {
                logger.LogInformation(
                    "{Module} - Processing outbox message {MessageId}. Content: {Content}",
                    ModuleName,
                    outboxMessage.Id,
                    outboxMessage.Content);

                // Log assembly and type information for debugging
                var assemblyInfo = AssemblyReference.Assembly.FullName;
                logger.LogInformation(
                    "{Module} - Current assembly info: {AssemblyInfo}",
                    ModuleName,
                    assemblyInfo);

                // Try to parse the type information from JSON without deserializing
                var jsonObject = JsonConvert.DeserializeObject<dynamic>(outboxMessage.Content);
                var typeString = (string)jsonObject?["$type"];
                
                if (typeString != null)
                {
                    logger.LogInformation(
                        "{Module} - Attempting to deserialize type: {TypeString}",
                        ModuleName,
                        typeString);

                    // Check if type exists in assembly
                    var type = AssemblyReference.Assembly.GetType(typeString);
                    logger.LogInformation(
                        "{Module} - Type found in assembly: {TypeFound}, Machine Name: {MachineName}",
                        ModuleName,
                        type != null,
                        Environment.MachineName);
                }

                IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    SerializerSettings.Instance)!;

                using IServiceScope scope = serviceScopeFactory.CreateScope();

                IEnumerable<IDomainEventHandler> domainEventHandlers = DomainEventHandlersFactory.GetHandlers(
                    domainEvent.GetType(),
                    scope.ServiceProvider,
                    AssemblyReference.Assembly);

                foreach (IDomainEventHandler domainEventHandler in domainEventHandlers)
                {
                    await domainEventHandler.Handle(domainEvent);

                    // Record the consumer after successful processing
                    await connection.ExecuteAsync(
                        """
                        INSERT INTO oms.outbox_messages_consumers (outbox_message_id, name)
                        VALUES (@OutboxMessageId, @Name)
                        """,
                        new
                        {
                            OutboxMessageId = outboxMessage.Id,
                            Name = domainEventHandler.GetType().Name
                        },
                        transaction: transaction);
                }
            }
            catch (Exception caughtException)
            {
                logger.LogError(
                    caughtException,
                    "{Module} - Exception while processing outbox message {MessageId}",
                    ModuleName,
                    outboxMessage.Id);

                exception = caughtException;
            }

            await UpdateOutboxMessageAsync(connection, transaction, outboxMessage, exception);
        }

        await transaction.CommitAsync();
    }

    private async Task<IReadOnlyList<OutboxMessageResponse>> GetOutboxMessagesAsync(
        IDbConnection connection,
        IDbTransaction transaction)
    {
        string sql =
            $"""
             SELECT
                id AS {nameof(OutboxMessageResponse.Id)},
                content AS {nameof(OutboxMessageResponse.Content)}
             FROM oms.outbox_messages
             WHERE processed_on_utc IS NULL
             ORDER BY occurred_on_utc
             LIMIT {outboxOptions.Value.BatchSize}
             FOR UPDATE
             """;

        IEnumerable<OutboxMessageResponse> outboxMessages = await connection.QueryAsync<OutboxMessageResponse>(
            sql,
            transaction: transaction);

        return outboxMessages.ToList();
    }

    private async Task UpdateOutboxMessageAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        OutboxMessageResponse outboxMessage,
        Exception? exception)
    {
        const string sql =
            """
            UPDATE oms.outbox_messages
            SET processed_on_utc = @ProcessedOnUtc,
                error = @Error
            WHERE id = @Id
            """;

        await connection.ExecuteAsync(
            sql,
            new
            {
                outboxMessage.Id,
                ProcessedOnUtc = dateTimeProvider.UtcNow,
                Error = exception?.ToString()
            },
            transaction: transaction);
    }

    internal sealed record OutboxMessageResponse(Guid Id, string Content);
}
