using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.CustomerSettings;

namespace Crowbond.Modules.CRM.Domain.Customers;

public sealed class Customer : Entity
{
    private Customer()
    {

    }

    public Guid Id { get; private set; }

    public string AccountNumber { get; private set; }

    public string BusinessName { get; private set; }

    public string BillingAddressLine1 { get; private set; }

    public string? BillingAddressLine2 { get; private set; }

    public string BillingTownCity { get; private set; }

    public string BillingCounty { get; private set; }

    public string BillingCountry { get; private set; }

    public string BillingPostalCode { get; private set; }

    public Guid PriceTierId { get; private set; }

    public decimal Discount { get; private set; }

    public Guid? RepId { get; private set; }

    public bool CustomPaymentTerm { get; private set; }

    public PaymentTerm? PaymentTerms { get; private set; }

    public int? InvoiceDueDays { get; private set; }

    public DeliveryFeeSetting DeliveryFeeSetting { get; private set; }

    public decimal? DeliveryMinOrderValue { get; private set; }

    public decimal? DeliveryCharge { get; private set; }

    public bool NoDiscountSpecialItem { get; private set; }

    public bool NoDiscountFixedPrice { get; private set; }

    public bool DetailedInvoice { get; private set; }

    public string? CustomerNotes { get; private set; }

    public bool IsHq { get; private set; }

    public bool IsActive { get; private set; }
    
    public CustomerSetting CustomerSetting { get; private set; }

    public Guid CreateBy { get; private set; }

    public DateTime CreateDate { get; private set; }

    public Guid? LastModifiedBy { get; private set; }

    public DateTime? LastModifiedDate { get; private set; }


    public static Result<Customer> Create(
         string accountNumber,
         string businessName,
         string billingAddressLine1,
         string? billingAddressLine2,
         string billingTownCity,
         string billingCounty,
         string billingCountry,
         string billingPostalCode,
         Guid priceTierId,
         decimal discount,
         Guid? repId,
         bool customPaymentTerm,
         PaymentTerm? paymentTerms,
         int? invoiceDueDays,
         DeliveryFeeSetting deliveryFeeSetting,
         decimal? deliveryMinOrderValue,
         decimal? deliveryCharge,
         bool noDiscountSpecialItem,
         bool noDiscountFixedPrice,
         bool detailedInvoice,
         string? customerNotes,
         bool isHq,
         bool showPricesInDeliveryDocket,
         bool showPriceInApp,
         ShowLogoInDeliveryDocket showLogoInDeliveryDocket,
         string? customerLogo,
         Guid createBy,
         DateTime createDate)
    {
        var customerId = Guid.NewGuid();

        var customer = new Customer
        {
            Id = customerId,
            AccountNumber = accountNumber,
            BusinessName = businessName,
            BillingAddressLine1 = billingAddressLine1,
            BillingAddressLine2 = billingAddressLine2,
            BillingTownCity = billingTownCity,
            BillingCounty = billingCounty,
            BillingCountry = billingCountry,
            BillingPostalCode = billingPostalCode,
            PriceTierId = priceTierId,
            Discount = discount,
            RepId = repId,
            CustomPaymentTerm = customPaymentTerm,
            PaymentTerms = paymentTerms,
            InvoiceDueDays = invoiceDueDays,
            DeliveryFeeSetting = deliveryFeeSetting,
            DeliveryMinOrderValue = deliveryMinOrderValue,
            DeliveryCharge = deliveryCharge,
            NoDiscountSpecialItem = noDiscountSpecialItem,
            NoDiscountFixedPrice = noDiscountFixedPrice,
            DetailedInvoice = detailedInvoice,
            CustomerNotes = customerNotes,
            IsHq = isHq,
            IsActive = true,
            CreateBy = createBy,
            CreateDate = createDate,
            CustomerSetting = CustomerSetting.Create(
                customerId: customerId,
                showPricesInDeliveryDocket: showPricesInDeliveryDocket,
                showPriceInApp: showPriceInApp,
                showLogoInDeliveryDocket: showLogoInDeliveryDocket,
                customerLogo: customerLogo)
        };

        return customer;
    }

    public void Update(
         string businessName,
         string billingAddressLine1,
         string? billingAddressLine2,
         string billingTownCity,
         string billingCounty,
         string billingCountry,
         string billingPostalCode,
         Guid priceTierId,
         decimal discount,
         Guid? repId,
         bool customPaymentTerm,
         PaymentTerm? paymentTerms,
         int? invoiceDueDays,
         DeliveryFeeSetting deliveryFeeSetting,
         decimal? deliveryMinOrderValue,
         decimal? deliveryCharge,
         bool noDiscountSpecialItem,
         bool noDiscountFixedPrice,
         bool detailedInvoice,
         string? customerNotes,
         Guid lastModifiedBy,
         DateTime lastModifiedDate,
         bool showPricesInDeliveryDocket,
         bool showPriceInApp,
         ShowLogoInDeliveryDocket showLogoInDeliveryDocket,
         string? customerLogo)
    {
        BusinessName = businessName;
        BillingAddressLine1 = billingAddressLine1;
        BillingAddressLine2 = billingAddressLine2;
        BillingTownCity = billingTownCity;
        BillingCounty = billingCounty;
        BillingCountry = billingCountry;
        BillingPostalCode = billingPostalCode;
        PriceTierId = priceTierId;
        Discount = discount;
        RepId = repId;
        CustomPaymentTerm = customPaymentTerm;
        PaymentTerms = paymentTerms;
        InvoiceDueDays = invoiceDueDays;
        DeliveryFeeSetting = deliveryFeeSetting;
        DeliveryMinOrderValue = deliveryMinOrderValue;
        DeliveryCharge = deliveryCharge;
        NoDiscountSpecialItem = noDiscountSpecialItem;
        NoDiscountFixedPrice = noDiscountFixedPrice;
        DetailedInvoice = detailedInvoice;
        CustomerNotes = customerNotes;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;        
        CustomerSetting.Update(
            showPricesInDeliveryDocket,
            showPriceInApp,
            showLogoInDeliveryDocket,
            customerLogo );        
    }

    public Result Activate(Guid lastModifiedBy, DateTime lastModifiedDate)
    {
        if (IsActive)
        {
            return Result.Failure(CustomerErrors.AlreadyActivated);
        }

        IsActive = true;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;

        return Result.Success();
    }

    public Result Deactivate(Guid lastModifiedBy, DateTime lastModifiedDate)
    {
        if (!IsActive)
        {
            return Result.Failure(CustomerErrors.AlreadyDeactivated);
        }

        IsActive = false;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;

        return Result.Success();
    }
}


