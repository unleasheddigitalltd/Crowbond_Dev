using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Orders;

public sealed class OrderLineRejectReason : Entity
{
    private OrderLineRejectReason()
    {        
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public OrderLineRejectResponsibility Responsibility { get; private set; }

    public bool IsActive { get; private set; }

    public static OrderLineRejectReason Create(string title, OrderLineRejectResponsibility responsibility)
    {
        var reason = new OrderLineRejectReason
        {
            Id = Guid.NewGuid(),
            Title = title,
            Responsibility = responsibility,
            IsActive = true
        };

        return reason;
    }

    public void Update(string title, OrderLineRejectResponsibility responsibility)
    {
        Title = title;
        Responsibility = responsibility;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
