using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Suppliers.GetSuppliers;

public sealed record GetSuppliersQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<SuppliersResponse>;
