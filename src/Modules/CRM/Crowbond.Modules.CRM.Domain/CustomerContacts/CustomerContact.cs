using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerContacts;

public sealed class CustomerContact : Entity
{
    private CustomerContact()
    {
    }

    public Guid Id { get; private set; }

    public Guid CustomerId { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string PhoneNumber { get; private set; }

    public string Mobile { get; private set; }

    public string Username { get; private set; }

    public string Email { get; private set; }

    public bool Primary { get; private set; }

    public bool ReceiveInvoice { get; private set; }

    public bool ReceiveOrder { get; private set; }

    public bool ReceivePriceList { get; private set; }

    public bool IsActive { get; private set; }

    public Guid CreateBy { get; private set; }

    public DateTime CreateDate { get; private set; }

    public Guid? LastModifiedBy { get; private set; }

    public DateTime? LastModifiedDate { get; private set; }

    public static CustomerContact Create(
        Guid customerId,
        string firstName,
        string lastName,
        string phoneNumber,
        string mobile,
        string email,
        string username,
        bool primary,
        bool receiveInvoice,
        bool receiveOrder,
        bool receivePriceList,
        Guid createBy,
        DateTime createDate)
    {
        var customerContact = new CustomerContact
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Mobile = mobile,
            Email = email,
            Username = username,
            Primary = primary,
            ReceiveInvoice = receiveInvoice,
            ReceiveOrder = receiveOrder,
            ReceivePriceList = receivePriceList,
            IsActive = true,
            CreateBy = createBy,
            CreateDate = createDate
        };


        customerContact.Raise(new CustomerContactCreatedDomainEvent(customerContact.Id));

        return customerContact;
    }

    public void Update(
        string firstName,
        string lastName,
        string phoneNumber,
        string mobile,
        bool primary,
        bool receiveInvoice,
        bool receiveOrder,
        bool receivePriceList,
        bool isActive,
        Guid lastModifiedBy,
        DateTime lastModifiedDate)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Mobile = mobile;
        Primary = primary;
        ReceiveInvoice = receiveInvoice;
        ReceiveOrder = receiveOrder;
        ReceivePriceList = receivePriceList;
        IsActive = isActive;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;

        Raise(new CustomerContactUpdatedDomainEvent(Id, FirstName, LastName));
    }

    public Result Activate(Guid lastModifiedBy, DateTime lastModifiedDate)
    {
        if (IsActive)
        {
            return Result.Failure(CustomerContactErrors.AlreadyActivated);
        }

        IsActive = true;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;

        Raise(new CustomerContactActivatedDomainEvent(Id));

        return Result.Success();
    }

    public Result Deactivate(Guid lastModifiedBy, DateTime lastModifiedDate)
    {
        if (!IsActive)
        {
            return Result.Failure(CustomerContactErrors.AlreadyDeactivated);
        }

        IsActive = false;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;

        Raise(new CustomerContactDeactivatedDomainEvent(Id));

        return Result.Success();
    }
}
