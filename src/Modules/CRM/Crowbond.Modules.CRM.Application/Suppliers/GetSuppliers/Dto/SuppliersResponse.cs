using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.CRM.Application.Suppliers.GetSuppliers.Dto;

public sealed class SuppliersResponse : PaginatedResponse<Supplier>
{

    public SuppliersResponse(IReadOnlyCollection<Supplier> Suppliers, IPagination pagination)
        : base(Suppliers, pagination)
    { }


}


