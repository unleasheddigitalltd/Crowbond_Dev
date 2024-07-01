using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.CRM.Application.Suppliers.GetSuppliers.Dto;

namespace Crowbond.Modules.CRM.Application.Suppliers.GetSuppliers;

public sealed record GetSuppliersQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<SuppliersResponse>;
