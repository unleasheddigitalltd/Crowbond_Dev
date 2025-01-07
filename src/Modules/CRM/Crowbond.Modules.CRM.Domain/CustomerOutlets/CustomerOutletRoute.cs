
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.Routes;

namespace Crowbond.Modules.CRM.Domain.CustomerOutlets;

public sealed class CustomerOutletRoute : Entity
{
    private CustomerOutletRoute()
    {
    }

    public Guid Id { get; private set; }

    public Guid CustomerOutletId { get; private set; }

    public Guid RouteId { get; private set; }

    public Weekday Weekday { get; private set; }

    internal static CustomerOutletRoute Create(Guid routeId, Weekday weekday)
    {
        var customerOutletRoute = new CustomerOutletRoute
        {
            Id = Guid.NewGuid(),
            RouteId = routeId,
            Weekday = weekday
        };

        return customerOutletRoute;
    }
}
