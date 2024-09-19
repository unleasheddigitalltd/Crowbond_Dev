using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Settings;

public sealed class Setting : Entity, ISoftDeletable
{
    public static readonly Setting Initial = new(new Guid("037b725f-2110-40f8-a1b3-06ca5722cb83"), 10);

    private Setting(Guid id, decimal deliveryCharge)
    {
        Id = id;
        DeliveryCharge = deliveryCharge;
    }

    private Setting()
    {
    }

    public Guid Id { get; private set; }

    public decimal DeliveryCharge { get; private set; }

    public bool IsDeleted { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOnUtc { get; set; }

    public static Setting Create(decimal deliveryCharge)
    {
        var setting = new Setting
        {
            DeliveryCharge = deliveryCharge
        };

        return setting;
    }
}
