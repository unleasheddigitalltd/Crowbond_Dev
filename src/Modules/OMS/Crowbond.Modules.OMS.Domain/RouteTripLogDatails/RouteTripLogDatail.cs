using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.RouteTripLogDatails;

public sealed class RouteTripLogDatail : Entity
{
    public RouteTripLogDatail()
    {    
    }

    public Guid Id { get; private set; }

    public Guid RouteTripLogId { get; private set; }

    public Guid OrderHeaderId { get; private set; }

    public RouteTripLogDatailStatus Status { get; private set; }

    public DateTime DateTime { get; private set; }


    public static RouteTripLogDatail Create(Guid routesTripLogId, Guid orderHeaderId, RouteTripLogDatailStatus status, DateTime dateTime)
    {
        var routesTripLogDatail = new RouteTripLogDatail
        {
            Id = Guid.NewGuid(),
            RouteTripLogId = routesTripLogId,
            OrderHeaderId = orderHeaderId,
            Status = status,
            DateTime = dateTime
        };

        return routesTripLogDatail;
    }
}
