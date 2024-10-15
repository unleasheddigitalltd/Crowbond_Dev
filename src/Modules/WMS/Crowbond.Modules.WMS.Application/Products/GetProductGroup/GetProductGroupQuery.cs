using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.GetProductGroup;

public sealed record GetProductGroupQuery(Guid ProductGroupId) : IQuery<ProductGroupResponse>;
