using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerContacts;

public sealed class CustomerContact : Entity , IAuditable, ISoftDeletable
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

    public bool IsPrimary { get; private set; }

    public bool ReceiveInvoice { get; private set; }

    public bool ReceiveOrder { get; private set; }

    public bool ReceivePriceList { get; private set; }

    public bool IsActive { get; private set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOnUtc { get; set; }

    public bool IsDeleted { get; set; }
    
    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOnUtc { get; set; }

    public static CustomerContact Create(
        Guid customerId,
        string firstName,
        string lastName,
        string phoneNumber,
        string mobile,
        string email,
        string username,
        bool receiveInvoice,
        bool receiveOrder,
        bool receivePriceList)
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
            ReceiveInvoice = receiveInvoice,
            ReceiveOrder = receiveOrder,
            ReceivePriceList = receivePriceList,
            IsPrimary = false,
            IsActive = true
        };


        customerContact.Raise(new CustomerContactCreatedDomainEvent(customerContact.Id));

        return customerContact;
    }

    public void Update(
        string firstName,
        string lastName,
        string phoneNumber,
        string mobile,
        bool receiveInvoice,
        bool receiveOrder,
        bool receivePriceList)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Mobile = mobile;
        ReceiveInvoice = receiveInvoice;
        ReceiveOrder = receiveOrder;
        ReceivePriceList = receivePriceList;

        Raise(new CustomerContactUpdatedDomainEvent(Id, FirstName, LastName));
    }

    public Result Activate()
    {
        if (IsActive)
        {
            return Result.Failure(CustomerContactErrors.AlreadyActivated);
        }

        IsActive = true;

        Raise(new CustomerContactActivatedDomainEvent(Id));

        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
        {
            return Result.Failure(CustomerContactErrors.AlreadyDeactivated);
        }

        if (IsPrimary)
        {
            return Result.Failure(CustomerContactErrors.IsPrimary);
        }

        IsActive = false;

        Raise(new CustomerContactDeactivatedDomainEvent(Id));

        return Result.Success();
    }

    public Result ChangePrimary(bool isPrimary)
    {
        if (!IsActive && isPrimary)
        {
            return Result.Failure(CustomerContactErrors.IsNotActive);
        }

        IsPrimary = isPrimary;

        return Result.Success();
    }
}
