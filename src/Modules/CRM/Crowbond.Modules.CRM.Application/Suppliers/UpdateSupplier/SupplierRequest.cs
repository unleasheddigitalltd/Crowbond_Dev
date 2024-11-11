using Crowbond.Modules.CRM.Domain.Settings;

namespace Crowbond.Modules.CRM.Application.Suppliers.UpdateSupplier;
public sealed record SupplierRequest(
     string SupplierName,
     string AddressLine1,
     string? AddressLine2,
     string TownCity,
     string County,
     string? Country,
     string PostalCode,
     string? SupplierNotes);

