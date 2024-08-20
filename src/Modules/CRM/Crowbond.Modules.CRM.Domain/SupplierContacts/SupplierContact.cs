using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.SupplierContacts;

public sealed class SupplierContact : Entity
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

    public bool Primary { get; private set; }

    public bool IsActive { get; private set; }

    public Guid CreateBy { get; private set; }

    public DateTime CreateDate { get; private set; }

    public Guid? LastModifiedBy { get; private set; }

    public DateTime? LastModifiedDate { get; private set; }

    public static SupplierContact Create(
        Guid supplierId,
        string firstName,
        string lastName,
        string phoneNumber,
        string? mobile,
        string email,
        string username,
        Guid createBy,
        DateTime createDate)
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
            Primary = false,
            IsActive = true,
            CreateBy = createBy,
            CreateDate = createDate
        };


        supplierContact.Raise(new SupplierContactCreatedDomainEvent(supplierContact.Id));

        return supplierContact;
    }

    public void Update(
        string firstName,
        string lastName,
        string phoneNumber,
        string? mobile,
        Guid lastModifiedBy,
        DateTime lastModifiedDate)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Mobile = mobile;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;

        Raise(new SupplierContactUpdatedDomainEvent(Id, FirstName, LastName));
    }

    public Result Activate(Guid lastModifiedBy, DateTime lastModifiedDate)
    {
        if (IsActive)
        {
            return Result.Failure(SupplierContactErrors.AlreadyActivated);
        }

        IsActive = true;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;

        Raise(new SupplierContactActivatedDomainEvent(Id));

        return Result.Success();
    }

    public Result Deactivate(Guid lastModifiedBy, DateTime lastModifiedDate)
    {
        if (!IsActive)
        {
            return Result.Failure(SupplierContactErrors.AlreadyDeactivated);
        }

        if (Primary)
        {
            return Result.Failure(SupplierContactErrors.IsPrimary);
        }

        IsActive = false;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;

        Raise(new SupplierContactDeactivatedDomainEvent(Id));

        return Result.Success();
    }

    public Result ChangePrimary(bool primary, Guid lastModifiedBy, DateTime lastModifiedDate)
    {
        if (!IsActive && primary)
        {
            return Result.Failure(SupplierContactErrors.IsNotActive);
        }

        Primary = primary;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;

        return Result.Success();
    }
}
