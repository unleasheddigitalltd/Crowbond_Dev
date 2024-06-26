using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Customers;

    public sealed class Customer : Entity
{
    public Customer()
    {

    }

    public Guid Id { get; set; }

    [Required]
    public int AccountNumber { get; set; }
    public string CompanyName { get; set; }

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

}


