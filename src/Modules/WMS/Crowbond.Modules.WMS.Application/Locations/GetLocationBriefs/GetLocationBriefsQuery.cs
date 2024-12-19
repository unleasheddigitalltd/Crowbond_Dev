using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Domain.Locations;

namespace Crowbond.Modules.WMS.Application.Locations.GetLocationBriefs;
public sealed record GetLocationBriefsQuery(LocationLayer LocationLayer, LocationType locationType) : IQuery<IReadOnlyCollection<LocationResponse>>;
