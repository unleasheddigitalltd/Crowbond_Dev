using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerOutlets;

public sealed class CustomerOutlet : Entity
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

    public Guid CreateBy { get; private set; }

    public DateTime CreateDate { get; private set; }

    public Guid? LastModifiedBy { get; private set; }

    public DateTime? LastModifiedDate { get; private set; }

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
        bool is24HrsDelivery,
        Guid createBy,
        DateTime createDate)
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
            IsActive = true,
            CreateBy = createBy,
            CreateDate = createDate
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
        bool is24HrsDelivery,
        Guid lastModifiedBy,
        DateTime lastModifiedDate)
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
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;
    }

    public Result Activate(Guid lastModifiedBy, DateTime lastModifiedDate)
    {
        if (IsActive)
        {
            return Result.Failure(CustomerOutletErrors.AlreadyActivated);
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
            return Result.Failure(CustomerOutletErrors.AlreadyDeactivated);
        }

        IsActive = false;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;

        return Result.Success();
    }
}
