namespace Crowbond.Modules.CRM.Application.Suppliers.GetSupplier;

public sealed record SupplierResponse(
   Guid Id,
   string AccountNumber,
   string SupplierName,
   string AddressLine1,
   string? AddressLine2,
   string TownCity,
   string County,
   string? Country,
   string PostalCode);
