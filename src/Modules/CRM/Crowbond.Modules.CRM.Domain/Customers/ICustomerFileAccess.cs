using Microsoft.AspNetCore.Http;

namespace Crowbond.Modules.CRM.Domain.Customers;

public interface ICustomerFileAccess
{
    public Task<string> SaveLogoAsync(string accountNumber, IFormFile logo, CancellationToken cancellationToken = default);

    public Task DeleteLogoAsync(string accountNumber, CancellationToken cancellationToken = default);
}
