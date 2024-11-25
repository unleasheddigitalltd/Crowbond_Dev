using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Presentation.Orders;

public sealed class OrderLineRejectReason: Entity
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public OrderLineRejectResponsibility Responsibility { get; set; }

    public bool IsActive { get; set; }

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
