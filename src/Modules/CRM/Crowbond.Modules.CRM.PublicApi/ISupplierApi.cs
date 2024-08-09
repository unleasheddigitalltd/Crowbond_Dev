namespace Crowbond.Modules.CRM.PublicApi;

public interface ISupplierApi
{
    Task<SupplierResponse?> GetAsync(Guid supplierId, CancellationToken cancellationToken = default);
}

public sealed record SupplierResponse(
    Guid Id,
    string AccountNumber,
    string SupplierName,
    string AddressLine1,
    string? AddressLine2,
    string TownCity,
    string County,
    string? Country,
    string PostalCode,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email);
