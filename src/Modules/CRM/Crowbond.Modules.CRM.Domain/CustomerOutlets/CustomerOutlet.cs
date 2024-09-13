using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerOutlets;

public sealed class CustomerOutlet : Entity , ISoftDeletable, IAuditable
{
    private CustomerOutlet()
    {
    }

    public Guid Id { get; private set; }

    public Guid CustomerId { get; private set; }

    public string LocationName { get; private set; }

    public string FullName { get; private set; }

    public string? Email { get; private set; }

    public string PhoneNumber { get; private set; }

    public string? Mobile { get; private set; }

    public string AddressLine1 { get; private set; }

    public string? AddressLine2 { get; private set; }

    public string TownCity { get; private set; }

    public string County { get; private set; }

    public string? Country { get; private set; }

    public string PostalCode { get; private set; }

    public string? DeliveryNote { get; private set; }

    public TimeOnly DeliveryTimeFrom { get; private set; }

    public TimeOnly DeliveryTimeTo { get; private set; }

    public bool Is24HrsDelivery { get; private set; }

    public bool IsActive { get; private set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOnUtc { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOnUtc { get; set; }

    public static CustomerOutlet Create(
        Guid customerId,
        string locationName,
        string fullName,
        string? email,
        string phoneNumber,
        string? mobile,
        string addressLine1,
        string? addressLine2,
        string townCity,
        string county,
        string? country,
        string postalCode,
        string? deliveryNote,
        TimeOnly deliveryTimeFrom,
        TimeOnly deliveryTimeTo,
        bool is24HrsDelivery)
    {
        var shippingAddress = new CustomerOutlet
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            LocationName = locationName,
            FullName = fullName,
            Email = email,
            PhoneNumber = phoneNumber,
            Mobile = mobile,
            AddressLine1 = addressLine1,
            AddressLine2 = addressLine2,
            TownCity = townCity,
            County = county,
            Country = country,
            PostalCode = postalCode,
            DeliveryNote = deliveryNote,
            DeliveryTimeFrom = deliveryTimeFrom,
            DeliveryTimeTo = deliveryTimeTo,
            Is24HrsDelivery = is24HrsDelivery,
            IsActive = true
        };
        return shippingAddress;
    }

    public void Update(
        string locationName,
        string fullName,
        string? email,
        string phoneNumber,
        string? mobile,
        string addressLine1,
        string? addressLine2,
        string townCity,
        string county,
        string? country,
        string postalCode,
        string? deliveryNote,
        TimeOnly deliveryTimeFrom,
        TimeOnly deliveryTimeTo,
        bool is24HrsDelivery)
    {
        LocationName = locationName;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        Mobile = mobile;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        TownCity = townCity;
        County = county;
        Country = country;
        PostalCode = postalCode;
        DeliveryNote = deliveryNote;
        DeliveryTimeFrom = deliveryTimeFrom;
        DeliveryTimeTo = deliveryTimeTo;
        Is24HrsDelivery = is24HrsDelivery;
    }

    public Result Activate()
    {
        if (IsActive)
        {
            return Result.Failure(CustomerOutletErrors.AlreadyActivated);
        }

        IsActive = true;

        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
        {
            return Result.Failure(CustomerOutletErrors.AlreadyDeactivated);
        }

        IsActive = false;

        return Result.Success();
    }
}
