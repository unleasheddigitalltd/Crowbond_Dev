using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Products.Application.Products.GetUnitOfMeasures.Dtos;

namespace Crowbond.Modules.Products.Application.Products.GetUnitOfMeasures;

public sealed record GetUnitOfMeasuresQuery() : IQuery<IReadOnlyCollection<UnitOfMeasureResponse>>;
