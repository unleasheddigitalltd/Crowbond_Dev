using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Drivers.GetDriverRouteTripAvtivation;

public sealed record GetDriverRouteTripAvtivationQuery(Guid DriverId) : IQuery<ActiveRouteTripResponse>;
