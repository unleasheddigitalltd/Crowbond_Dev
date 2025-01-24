using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Drivers.GetDriverRouteTripAvtivation;
using ActiveRouteTripResponse = Crowbond.Modules.OMS.PublicApi.ActiveRouteTripResponse;
using Crowbond.Modules.OMS.PublicApi;
using MediatR;

namespace Crowbond.Modules.OMS.Infrastructure.PublicApi;

internal sealed class DriverApi(ISender sender) : IDriverApi
{
    public async Task<ActiveRouteTripResponse> GetDriverActiveRouteTripAsync(Guid driverId, CancellationToken cancellationToken = default)
    {
        Result<Application.Drivers.GetDriverRouteTripAvtivation.ActiveRouteTripResponse> result = 
            await sender.Send(new GetDriverRouteTripAvtivationQuery(driverId), cancellationToken);

        if (result.IsFailure)
        {
            throw new InvalidOperationException(result.Error.Description);
        }

        return new ActiveRouteTripResponse(
            result.Value.DriverId,
            result.Value.ActiveRouteTripId,
            result.Value.ActiveRouteName);
    }
}
