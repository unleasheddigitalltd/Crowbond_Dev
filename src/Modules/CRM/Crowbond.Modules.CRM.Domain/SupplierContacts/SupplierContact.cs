using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.SupplierContacts;

public sealed class SupplierContact : Entity, ISoftDeletable, IAuditable
{
    private SupplierContact()
    {        
    }

    public Guid Id { get; private set; }

    public Guid SupplierId { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string PhoneNumber { get; private set; }

    public string? Mobile { get; private set; }

    public string Username { get; private set; }

    public string Email { get; private set; }

    public bool IsPrimary { get; private set; }

    public bool IsActive { get; private set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOnUtc { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOnUtc { get; set; }

    public static SupplierContact Create(
        Guid supplierId,
        string firstName,
        string lastName,
        string phoneNumber,
        string? mobile,
        string email,
        string username)
    {
        var supplierContact = new SupplierContact
        {
            Id = Guid.NewGuid(),
            SupplierId = supplierId,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Mobile = mobile,
            Email = email,
            Username = username,
            IsPrimary = false,
            IsActive = true
        };


        supplierContact.Raise(new SupplierContactCreatedDomainEvent(supplierContact.Id));

        return supplierContact;
    }

    public void Update(
        string firstName,
        string lastName,
        string phoneNumber,
        string? mobile)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Mobile = mobile;

        Raise(new SupplierContactUpdatedDomainEvent(Id, FirstName, LastName));
    }

    public Result Activate()
    {
        if (IsActive)
        {
            return Result.Failure(SupplierContactErrors.AlreadyActivated);
        }

        IsActive = true;

        Raise(new SupplierContactActivatedDomainEvent(Id));

        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
        {
            return Result.Failure(SupplierContactErrors.AlreadyDeactivated);
        }

        if (IsPrimary)
        {
            return Result.Failure(SupplierContactErrors.IsPrimary);
        }

        IsActive = false;

        Raise(new SupplierContactDeactivatedDomainEvent(Id));

        return Result.Success();
    }

    public Result ChangePrimary(bool isPrimary)
    {
        if (!IsActive && isPrimary)
        {
            return Result.Failure(SupplierContactErrors.IsNotActive);
        }

        IsPrimary = isPrimary;

        return Result.Success();
    }
}
