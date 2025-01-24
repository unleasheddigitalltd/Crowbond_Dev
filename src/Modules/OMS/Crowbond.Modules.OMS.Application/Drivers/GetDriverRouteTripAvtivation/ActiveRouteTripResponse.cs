namespace Crowbond.Modules.OMS.Application.Drivers.GetDriverRouteTripAvtivation;

public sealed record ActiveRouteTripResponse(Guid DriverId, Guid? ActiveRouteTripId, string? ActiveRouteName);
