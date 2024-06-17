using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Products.GetUnitOfMeasures.Dtos;

namespace Crowbond.Modules.WMS.Application.Products.GetUnitOfMeasures;

public sealed record GetUnitOfMeasuresQuery() : IQuery<IReadOnlyCollection<UnitOfMeasureResponse>>;
