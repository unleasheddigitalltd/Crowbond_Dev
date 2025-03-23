namespace Crowbond.Modules.OMS.PublicApi;

public interface IDriverApi
{
    Task<ActiveRouteTripResponse> GetDriverActiveRouteTripAsync(Guid driverId, CancellationToken cancellationToken = default);
}

public sealed record ActiveRouteTripResponse(Guid DriverId, Guid? ActiveRouteTripId, string? ActiveRouteName);


public interface IRouteTripApi
{
    Task AssignRouteTripToOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
}
