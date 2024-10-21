using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.GetProductGroups;

public sealed record GetProductGroupsQuery() : IQuery<IReadOnlyCollection<ProductGroupResponse>>;
