using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Dispatches;
public static class DispatchErrors
{
    public static Error NotFound(Guid dispatchId) =>
        Error.NotFound("Dispatches.NotFound", $"The dispatch with the identifier {dispatchId} was not found");

    public static Error ForRouteTripNotFound(Guid routeTripId) =>
        Error.NotFound("Dispatches.ForRouteTripNotFound", $"The dispatch for the route trip with the identifier {routeTripId} was not found");
    
    public static Error LineAlreadyPicked(Guid dispatchLineId) =>
        Error.NotFound("Dispatches.LineAlreadyPicked", $"The dispatch line with the identifier {dispatchLineId} was already picked");

}
