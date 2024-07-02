using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Suppliers;

    public sealed class Supplier : Entity
{
    public Supplier()
    {

    }

    public Guid Id { get; }
    public int AccountNumber { get; set; }
    public string SupplierName { get; set; }

    public string AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string AddressTownCity { get; set; }

    public string AddressCounty { get; set; }

    public string? AddressCountry { get; set; }

    public string AddressPostalCode { get; set; }

    public string BillingAddressLine1 { get; set; }

    public string? BillingAddressLine2 { get; set; }

    public string BillingAddressTownCity { get; set; }

    public string BillingAddressCounty { get; set; }

    public string? BillingAddressCountry { get; set; }

    public string BillingAddressPostalCode { get; set; }

    public string SupplierEmail { get; set; }

    public string SupplierPhone { get; set; }

    public string SupplierContact { get; set; }

    public int PaymentTerms { get; set; }

    public string? SupplierNotes { get; set; }


    public static Result<Supplier> Create(
     int accountnumber,
     string suppliername,
     string addressline1,
     string? addressline2,
     string towncity,
     string county,
     string? country,
     string postalcode,
     string billingaddressline1,
     string? billingaddressline2,
     string billingaddresstowncity,
     string billingaddresscounty,
     string? billingaddresscountry,
     string billingaddresspostalcode,
     int paymentterms,
     string? suppliernotes,
    string supplieremail,
    string supplierphone,
    string suppliercontact
)
    {
        var supplier = new Supplier
        {
            AccountNumber = accountnumber,
            SupplierName = suppliername,
            AddressLine1 = addressline1,
            AddressLine2 = addressline2,
            AddressTownCity = towncity,
            AddressCounty = county,
            AddressCountry = country,
            AddressPostalCode = postalcode,
            BillingAddressLine1 = billingaddressline1,
            BillingAddressLine2 = billingaddressline2,
            BillingAddressTownCity = billingaddresstowncity,
            BillingAddressCounty = billingaddresscounty,
            BillingAddressCountry = billingaddresscountry,
            BillingAddressPostalCode = billingaddresspostalcode,
            PaymentTerms = paymentterms,
            SupplierNotes = suppliernotes,
            SupplierEmail = supplieremail,
            SupplierPhone = supplierphone,
            SupplierContact = suppliercontact
        };

        return supplier;
    }

    public void Update(
         string suppliername,
         string addressline1,
         string? addressline2,
         string addresstowncity,
         string addresscounty,
         string? addresscountry,
         string addresspostalcode,
         string billingaddressline1,
         string? billingaddressline2,
         string billingaddresstowncity,
         string billingaddresscounty,
         string? billingaddresscountry,
         string billingaddresspostalcode,
         int paymentterms,
         string? suppliernotes,
        string supplieremail,
        string supplierphone,
        string suppliercontact)
    {

        SupplierName = suppliername;
        AddressLine1 = addressline1;
        AddressLine2 = addressline2;
        AddressTownCity = addresstowncity;
        AddressCounty = addresscounty;
        AddressCountry = addresscountry;
        AddressPostalCode = addresspostalcode;
        BillingAddressLine1 = billingaddressline1;
        BillingAddressLine2 = billingaddressline2;
        BillingAddressTownCity = billingaddresstowncity;
        BillingAddressCounty = billingaddresscounty;
        BillingAddressCountry = billingaddresscountry;
        BillingAddressPostalCode = billingaddresspostalcode;
        PaymentTerms = paymentterms;
        SupplierNotes = suppliernotes;
        SupplierEmail = supplieremail;
        SupplierPhone = supplierphone;
        SupplierContact = suppliercontact;
    }

}


