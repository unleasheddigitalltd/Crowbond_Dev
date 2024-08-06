﻿using Crowbond.Modules.CRM.Domain.Suppliers;

namespace Crowbond.Modules.CRM.Domain.SupplierContacts;

public interface ISupplierContactRepository
{
    Task<SupplierContact?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<SupplierContact>> GetForCustomerAsync(Supplier supplier, CancellationToken cancellationToken = default);
    void Insert(SupplierContact contact);
    void InsertRange(IEnumerable<SupplierContact> contacts);
}