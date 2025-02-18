﻿using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.Settings;

namespace Crowbond.Modules.CRM.Domain.Suppliers;

public sealed class Supplier : Entity
{
    private Supplier()
    {
    }

    public Guid Id { get; private set; }

    public string AccountNumber { get; private set; }

    public string SupplierName { get; private set; }

    public string AddressLine1 { get; private set; }

    public string? AddressLine2 { get; private set; }

    public string TownCity { get; private set; }

    public string County { get; private set; }

    public string? Country { get; private set; }

    public string PostalCode { get; private set; }

    public bool IsActive { get; private set; }

    public string? SupplierNotes { get; private set; }

    public Guid CreateBy { get; private set; }

    public DateTime CreateDate { get; private set; }

    public Guid? LastModifiedBy { get; private set; }

    public DateTime? LastModifiedDate { get; private set; }


    public static Result<Supplier> Create(
     string accountNumber,
     string supplierName,
     string addressLine1,
     string? addressLine2,
     string townCity,
     string county,
     string? country,
     string postalCode,
     string? supplierNotes)
    {
        var supplier = new Supplier
        {
            Id = Guid.NewGuid(),
            AccountNumber = accountNumber,
            SupplierName = supplierName,
            AddressLine1 = addressLine1,
            AddressLine2 = addressLine2,
            TownCity = townCity,
            County = county,
            Country = country,
            PostalCode = postalCode,
            IsActive = true,
            SupplierNotes = supplierNotes
        };

        return supplier;
    }

    public void Update(
         string suppliername,
         string addressline1,
         string? addressline2,
         string towncity,
         string county,
         string? country,
         string postalcode,
         string? suppliernotes)
    {

        SupplierName = suppliername;
        AddressLine1 = addressline1;
        AddressLine2 = addressline2;
        TownCity = towncity;
        County = county;
        Country = country;
        PostalCode = postalcode;
        SupplierNotes = suppliernotes;
    }

    public Result Activate()
    {
        if (IsActive)
        {
            return Result.Failure(SupplierErrors.AlreadyActivated);
        }

        IsActive = true;

        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
        {
            return Result.Failure(SupplierErrors.AlreadyDeactivated);
        }

        IsActive = false;

        return Result.Success();
    }
}


