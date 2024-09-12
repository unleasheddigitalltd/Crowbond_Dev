using System.Data;
using System.Data.Common;
using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerProductPriceUpdating;

[DisallowConcurrentExecution]
internal sealed class ProcessCustomerProductPriceUpdatingJob(
    IDbConnectionFactory dbConnectionFactory,
    IDateTimeProvider dateTimeProvider,
    IOptions<CustomerProductPriceUpdatingOptions> pricingOptions,
    ILogger<ProcessCustomerProductPriceUpdatingJob> logger) : IJob
{
    private const string ModuleName = "crm";
    public async Task Execute(IJobExecutionContext context)
    {
        var today = DateOnly.FromDateTime(dateTimeProvider.UtcNow);

        logger.LogInformation("{Module} - Beginning to process customer price updating messages", ModuleName);

        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        await using DbTransaction transaction = await connection.BeginTransactionAsync();

        IReadOnlyList<CustomerProductPriceResponse> customerProductPrices = await GetCustomerProductAsync(today, connection, transaction);

        foreach (CustomerProductPriceResponse customerProductPrice in customerProductPrices)
        {
            Exception? exception = null;

            bool isActive;
            bool isDeleted;

            try
            {
                isActive = customerProductPrice.EffectiveDate <= today;
                isDeleted = customerProductPrice.ExpiryDate <= today;

                customerProductPrice.UpdateStatus(isActive, isDeleted);
            }
            catch (Exception caughtException)
            {
                logger.LogError(
                    caughtException,
                    "Exception while processing pricing message {MessageId}",
                    customerProductPrice.Id);

                exception = caughtException;
            }

            await UpdateCustomerProductAsync(connection, transaction, customerProductPrice, exception);
        }

        await transaction.CommitAsync();

        logger.LogInformation("{Module} - Completed processing customer price updating messages", ModuleName);
    }

    private async Task<IReadOnlyList<CustomerProductPriceResponse>> GetCustomerProductAsync(
        DateOnly today,
        IDbConnection connection,
        IDbTransaction transaction)
    {
        string sql =
            $"""
             SELECT
                id AS {nameof(CustomerProductPriceResponse.Id)},
                effective_date AS {nameof(CustomerProductPriceResponse.EffectiveDate)},
                expiry_date AS {nameof(CustomerProductPriceResponse.ExpiryDate)},
                is_active AS {nameof(CustomerProductPriceResponse.IsActive)},
                is_deleted AS {nameof(CustomerProductPriceResponse.IsDeleted)}
             FROM crm.customer_product_prices
             WHERE (processed_on_utc IS NULL 
             OR processed_on_utc < @Today)
             AND is_deleted = false
             ORDER BY created_on_utc
             LIMIT {pricingOptions.Value.BatchSize}
             FOR UPDATE
             """;

        IEnumerable<CustomerProductPriceResponse> inboxMessages = await connection.QueryAsync<CustomerProductPriceResponse>(
            sql,
            new { Today = today.ToDateTime(TimeOnly.MinValue) },
            transaction: transaction);

        return inboxMessages.AsList();
    }

    private async Task UpdateCustomerProductAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        CustomerProductPriceResponse customerProduct,
        Exception? exception)
    {
        string sql =
            $"""
            UPDATE crm.customer_product_prices
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

    internal sealed class CustomerProductPriceResponse
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
