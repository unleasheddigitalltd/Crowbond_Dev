using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Dispatches;
public static class DispatchErrors
{
    public static Error NotFound(Guid dispatchId) =>
        Error.NotFound("Dispatches.NotFound", $"The dispatch with the identifier {dispatchId} was not found");
    public static Error LineNotFound(Guid dispatchId) =>
        Error.NotFound("Dispatches.LineNotFound", $"The dispatch line with the identifier {dispatchId} was not found");

    public static Error HasNoLines(Guid dispatchId) =>
        Error.NotFound("Dispatches.HasNoLines", $"The dispatch header with the identifier {dispatchId} has no lines");

    public static readonly Error QuantityMismatch =
        Error.Problem("Dispatches.QuantityMismatch", "The picked quantity does not match the ordered quantity.");
    
    public static readonly Error PickedExceedsOrdered =
        Error.Problem("Dispatches.PickedExceedsOrdered", "The recorded picked quantity exceeds the actual ordered quantity");

    public static Error ForRouteTripNotFound(Guid routeTripId) =>
        Error.NotFound("Dispatches.ForRouteTripNotFound", $"The dispatch for the route trip with the identifier {routeTripId} was not found");
    
    public static Error LineAlreadyPicked(Guid dispatchLineId) =>
        Error.NotFound("Dispatches.LineAlreadyPicked", $"The dispatch line with the identifier {dispatchLineId} was already picked");
    
    public static Error LineAlreadyChecked(Guid dispatchLineId) =>
        Error.NotFound("Dispatches.LineAlreadyChecked", $"The dispatch line with the identifier {dispatchLineId} was already checked");
    
    public static Error LineNotPicked(Guid dispatchLineId) =>
        Error.NotFound("Dispatches.LineNotPicked", $"The dispatch line with the identifier {dispatchLineId} was not picked");


    public static readonly Error NotProcessing = Error.Problem("Receipts.NotProcessing", "The dispatch is not in processing status");

    public static readonly Error NotNotAvailableForPicking = Error.Problem("Receipts.NotNotAvailableForPicking", "The dispatch is not in available status for picking");

}
