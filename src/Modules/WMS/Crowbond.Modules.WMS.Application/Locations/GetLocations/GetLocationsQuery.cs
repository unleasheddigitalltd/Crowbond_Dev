using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Locations.GetLocations;

public sealed record GetLocationsQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<LocationsResponse>;
