using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Settings;
public sealed class Setting : Entity, ISoftDeletable
{
    public static readonly Setting Initial = new (new Guid("037b725f-2110-40f8-a1b3-06ca5722cb83"), false);

    private Setting(Guid id, bool hasMixBatchLocation)
    {
        Id = id;
        HasMixBatchLocation = hasMixBatchLocation;
    }

    private Setting()
    {        
    }

    public Guid Id { get; private set; }
    public bool HasMixBatchLocation { get; private set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }

    public static Setting Create(bool hasMixBatchLocation)
    {
        var setting = new Setting
        {
            HasMixBatchLocation = hasMixBatchLocation
        };

        return setting;
    }
}
