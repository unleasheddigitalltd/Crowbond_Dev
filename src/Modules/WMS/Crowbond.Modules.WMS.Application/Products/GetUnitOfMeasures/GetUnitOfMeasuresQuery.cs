using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.GetUnitOfMeasures;

public sealed record GetUnitOfMeasuresQuery() : IQuery<IReadOnlyCollection<UnitOfMeasureResponse>>;
