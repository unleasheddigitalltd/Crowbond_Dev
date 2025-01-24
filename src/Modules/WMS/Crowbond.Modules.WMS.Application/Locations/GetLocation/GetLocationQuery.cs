using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Locations.GetLocation;

public sealed record GetLocationQuery(Guid LocationId) : IQuery<LocationResponse>;
