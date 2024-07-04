using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Locations.GetLocations;
public sealed record GetLocationsQuery(string LocationType) : IQuery<IReadOnlyCollection<LocationResponse>>;
