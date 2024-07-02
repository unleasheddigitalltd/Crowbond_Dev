using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Settings;
public sealed class Setting : Entity
{
    public Setting()
    {
        
    }

    public Guid Id { get; set; }

    public bool HasMixBatchLocation { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsActive { get; set; }
}
