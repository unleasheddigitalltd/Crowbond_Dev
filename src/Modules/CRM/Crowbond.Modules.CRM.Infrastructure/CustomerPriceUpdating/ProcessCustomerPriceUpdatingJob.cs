using System.Data;
using System.Data.Common;
using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerPriceUpdating;

[DisallowConcurrentExecution]
internal sealed class ProcessCustomerPriceUpdatingJob(
    IDbConnectionFactory dbConnectionFactory,
    IDateTimeProvider dateTimeProvider,
    IOptions<CustomerPriceUpdatingOptions> pricingOptions,
    ILogger<ProcessCustomerPriceUpdatingJob> logger) : IJob
{
    private const string ModuleName = "crm";
    public async Task Execute(IJobExecutionContext context)
    {
        var today = DateOnly.FromDateTime(dateTimeProvider.UtcNow);

        logger.LogInformation("{Module} - Beginning to process customer price updating messages", ModuleName);

        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        await using DbTransaction transaction = await connection.BeginTransactionAsync();

        IReadOnlyList<CustomerProductResponse> customerProducts = await GetCustomerProductAsync(today, connection, transaction);

        foreach (CustomerProductResponse customerProduct in customerProducts)
        {
            Exception? exception = null;

            bool isActive;
            bool isDeleted;

            try
            {
                isActive = customerProduct.EffectiveDate <= today;
                isDeleted = customerProduct.ExpiryDate <= today;

                customerProduct.UpdateStatus(isActive, isDeleted);
            }
            catch (Exception caughtException)
            {
                logger.LogError(
                    caughtException,
                    "Exception while processing pricing message {MessageId}",
                    customerProduct.Id);

                exception = caughtException;
            }

            await UpdateCustomerProductAsync(connection, transaction, customerProduct, exception);
        }

        await transaction.CommitAsync();

        logger.LogInformation("{Module} - Completed processing customer price updating messages", ModuleName);
    }

    private async Task<IReadOnlyList<CustomerProductResponse>> GetCustomerProductAsync(
        DateOnly today,
        IDbConnection connection,
        IDbTransaction transaction)
    {
        string sql =
            $"""
             SELECT
                id AS {nameof(CustomerProductResponse.Id)},
                effective_date AS {nameof(CustomerProductResponse.EffectiveDate)},
                expiry_date AS {nameof(CustomerProductResponse.ExpiryDate)},
                is_active AS {nameof(CustomerProductResponse.IsActive)},
                is_deleted AS {nameof(CustomerProductResponse.IsDeleted)}
             FROM crm.customer_products
             WHERE (processed_on_utc IS NULL 
             OR processed_on_utc < @Today)
             AND is_deleted = false
             ORDER BY created_on_utc
             LIMIT {pricingOptions.Value.BatchSize}
             FOR UPDATE
             """;

        IEnumerable<CustomerProductResponse> inboxMessages = await connection.QueryAsync<CustomerProductResponse>(
            sql,
            new { Today = today.ToDateTime(TimeOnly.MinValue) },
            transaction: transaction);

        return inboxMessages.AsList();
    }

    private async Task UpdateCustomerProductAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        CustomerProductResponse customerProduct,
        Exception? exception)
    {
        string sql =
            $"""
            UPDATE crm.customer_products
            SET 
            is_active = @IsActive,
            is_deleted = @IsDeleted,
            processed_on_utc = @ProcessedOnUtc,
            error_message = @Error
            WHERE id = @Id
            """;

        await connection.ExecuteAsync(
            sql,
            new
            {
                customerProduct.Id,
                customerProduct.IsActive,
                customerProduct.IsDeleted,
                ProcessedOnUtc = dateTimeProvider.UtcNow,
                Error = exception?.ToString()
            },
            transaction: transaction);
    }

    internal sealed class CustomerProductResponse
    {
        public Guid Id { get; init; }
        public DateOnly EffectiveDate { get; init; }
        public DateOnly? ExpiryDate { get; init; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public void UpdateStatus(bool isActive, bool isDeleted)
        {
            IsActive = isActive;
            IsDeleted = isDeleted;
        }
    };
}
