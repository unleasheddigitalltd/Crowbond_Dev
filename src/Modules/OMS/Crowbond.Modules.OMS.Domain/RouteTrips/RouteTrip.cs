using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.RouteTripLogs;

namespace Crowbond.Modules.OMS.Domain.RouteTrips;

public sealed class RouteTrip : Entity
{
    private readonly List<RouteTripStatusHistory> _statusHistories = new();
    private readonly List<RouteTripLog> _logs = new();

    private RouteTrip()
    {        
    }

    public Guid Id { get; private set; }

    public DateOnly Date {  get; private set; }

    public Guid RouteId { get; private set; }

    public string? Comments { get; private set; }

    public RouteTripStatus Status { get; private set; }

    public IReadOnlyCollection<RouteTripStatusHistory> StatusHistories => _statusHistories;

    public IReadOnlyCollection<RouteTripLog> Logs => _logs;

    public static RouteTrip Create(DateOnly date, Guid routeId, string? comments = null)
    {
        var routeTrip = new RouteTrip
        {
            Id = Guid.NewGuid(),
            Date = date,
            RouteId = routeId,
            Comments = comments,
            Status = RouteTripStatus.Registered
        };

        return routeTrip;
    }

    public Result Approve(DateOnly utcNow)
    {
        if (!CanApprove() && !IsExpired(utcNow))
        {
            return Result.Failure(RouteTripErrors.InvalidStatus(RouteTripStatus.Registered));
        }

        Status = RouteTripStatus.Approved;

        Raise(new RouteTripApprovedDomainEvent(Id));
        return Result.Success();
    }

    public bool IsExpired(DateOnly utcNow)
    {
        return Status == RouteTripStatus.Registered && Date < utcNow;
    }

    public bool CanApprove()
    {
        return Status == RouteTripStatus.Registered;
    }

}
