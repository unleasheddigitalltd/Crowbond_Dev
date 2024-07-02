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


    public static Result<Customer> Create(
         int accountnumber,
         string businessname,
         string? drivercode,
         string shippingaddressline1,
         string? shippingaddressline2,
         string shippingtowncity,
         string shippingcounty,
         string? shippingcountry,
         string shippingpostalcode,
         string billingaddressline1,
         string? billingaddressline2,
         string billingtowncity,
         string billingcounty,
         string billingcountry,
         string billingpostalcode,
         int pricegroupid,
         int paymentterms,
         string? customernotes,
        string customeremail,
        string customerphone,
        string customercontact
    )
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            AccountNumber = accountnumber,
            BusinessName = businessname,
            DriverCode = drivercode,
            ShippingAddressLine1 = shippingaddressline1,
            ShippingAddressLine2 = shippingaddressline2,
            ShippingTownCity = shippingtowncity,
            ShippingCounty = shippingcounty,
            ShippingCountry = shippingcountry,
            ShippingPostalCode = shippingpostalcode,
            BillingAddressLine1 = billingaddressline1,
            BillingAddressLine2 = billingaddressline2,
            BillingTownCity = billingtowncity,
            BillingCounty = billingcounty,
            BillingCountry = billingcountry,
            BillingPostalCode = billingpostalcode,
            PriceGroupId = pricegroupid,
            PaymentTerms = paymentterms,
            CustomerNotes = customernotes,
            CustomerEmail = customeremail,
            CustomerPhone = customerphone,
            CustomerContact = customercontact
        };

        return customer;
    }

    public void Update(
         string businessname,
         string? drivercode,
         string shippingaddressline1,
         string? shippingaddressline2,
         string shippingtowncity,
         string shippingcounty,
         string? shippingcountry,
         string shippingpostalcode,
         string billingaddressline1,
         string? billingaddressline2,
         string billingtowncity,
         string billingcounty,
         string billingcountry,
         string billingpostalcode,
         int pricegroupid,
         int paymentterms,
         string? customernotes,
        string customeremail,
        string customerphone,
        string customercontact)
    {

        BusinessName = businessname;
        DriverCode = drivercode;
        ShippingAddressLine1 = shippingaddressline1;
        ShippingAddressLine2 = shippingaddressline2;
        ShippingTownCity = shippingtowncity;
        ShippingCounty = shippingcounty;
        ShippingCountry = shippingcountry;
        ShippingPostalCode = shippingpostalcode;
        BillingAddressLine1 = billingaddressline1;
        BillingAddressLine2 = billingaddressline2;
        BillingTownCity = billingtowncity;
        BillingCounty = billingcounty;
        BillingCountry = billingcountry;
        BillingPostalCode = billingpostalcode;
        PriceGroupId = pricegroupid;
        PaymentTerms = paymentterms;
        CustomerNotes = customernotes;
        CustomerEmail = customeremail;
        CustomerPhone = customerphone;
        CustomerContact = customercontact;
    }
}


