using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crowbond.Modules.CRM.Application.Customers.UpdateCustomer;
public sealed record CustomerDto(
    Guid? Id,
     int AccountNumber,
     string BusinessName,
     string? DriverCode,
     string ShippingAddressLine1,
     string? ShippingAddressLine2,
     string ShippingTownCity,
     string ShippingCounty,
     string? ShippingCountry,
     string ShippingPostalCode,
     string BillingAddressLine1,
     string? BillingAddressLine2,
     string BillingTownCity,
     string BillingCounty,
     string BillingCountry,
     string BillingPostalCode,
     int PriceGroupId,
     int PaymentTerms,
     string? CustomerNotes,
     string CustomerEmail,
     string CustomerPhone,
     string CustomerContact
    );
