﻿using System.Xml.Linq;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerDetails;

public sealed record CustomerDetailsResponse()
{
    public Guid Id { get; }

    public int AccountNumber { get; set; }
    public string BusinessName { get; set; }

    public string? DriverCode { get; set; }

    public string ShippingAddressLine1 { get; set; }

    public string? ShippingAddressLine2 { get; set; }

    public string ShippingTownCity { get; set; }

    public string ShippingCounty { get; set; }

    public string? ShippingCountry { get; set; }

    public string ShippingPostalCode { get; set; }

    public string BillingAddressLine1 { get; set; }

    public string? BillingAddressLine2 { get; set; }

    public string BillingTownCity { get; set; }

    public string BillingCounty { get; set; }

    public string BillingCountry { get; set; }

    public string BillingPostalCode { get; set; }

    public int PriceGroupId { get; set; }

    public Guid InvoicePeriodId { get; set; }

    public int PaymentTerms { get; set; }

    public bool DetailedInvoice { get; set; }

    public string? CustomerNotes { get; set; }

    public string CustomerEmail { get; set; }

    public string CustomerPhone { get; set; }

    public string CustomerContact { get; set; }
};
