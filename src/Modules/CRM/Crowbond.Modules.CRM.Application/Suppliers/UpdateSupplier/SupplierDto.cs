using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crowbond.Modules.CRM.Application.Suppliers.UpdateSupplier;
public sealed record SupplierDto(
    Guid? Id,
     int AccountNumber,
     string SupplierName,
     string AddressLine1,
     string? AddressLine2,
     string AddressTownCity,
     string AddressCounty,
     string? AddressCountry,
     string AddressPostalCode,
     string BillingAddressLine1,
     string? BillingAddressLine2,
     string BillingAddressTownCity,
     string BillingAddressCounty,
     string BillingAddressCountry,
     string BillingAddressPostalCode,
     int PaymentTerms,
     string? SupplierNotes,
     string SupplierEmail,
     string SupplierPhone,
     string SupplierContact
    );
