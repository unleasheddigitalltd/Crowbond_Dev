using System;
using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerOutletRoutes;

public sealed class CustomerOutletRoute : Entity
{
    public CustomerOutletRoute()
    {
        
    }

    public Guid Id { get; private set; }
    public Guid CustomerOutletId { get; private set; }
    public Guid RouteId { get; private set; }
    public string DaysOfWeek { get; private set; }

    public static CustomerOutletRoute Create(Guid customerOutletId, Guid routeId, string daysOfWeek)
    {
        var customerOutletRoute = new CustomerOutletRoute
        {
            Id = Guid.NewGuid(),
            CustomerOutletId = customerOutletId,
            RouteId = routeId,
            DaysOfWeek = daysOfWeek
        };

        return customerOutletRoute;
    }

    public void Update(Guid routeId, string daysOfWeek)
    {
        RouteId = routeId;
        DaysOfWeek = daysOfWeek;
    }
}
