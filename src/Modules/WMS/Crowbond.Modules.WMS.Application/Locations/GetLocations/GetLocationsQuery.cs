using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Domain.Locations;

namespace Crowbond.Modules.WMS.Application.Locations.GetLocations;
public sealed record GetLocationsQuery(LocationType locationType) : IQuery<IReadOnlyCollection<LocationResponse>>;
