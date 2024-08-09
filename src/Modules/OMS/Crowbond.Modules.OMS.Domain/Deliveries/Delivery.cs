using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Deliveries;

public sealed class Delivery : Entity
{
    private Delivery()
    {        
    }

    public Guid Id { get; private set; }

    public Guid RouteTripLogDetailId {  get; private set; }

    public DateTime DateTime { get; private set; }

    public string? Comments { get; private set; }

    public static Delivery Create(Guid routeTripLogDetailId, DateTime dateTime, string? Comments)
    {
        var delivery = new Delivery
        {
            Id = Guid.NewGuid(),
            RouteTripLogDetailId = routeTripLogDetailId,
            DateTime = dateTime,
            Comments = Comments
        };

        return delivery;
    }

    public void Update(string comments)
    {
        Comments = comments.Trim();
    }
}
