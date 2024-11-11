using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Compliances;

public sealed class ComplianceLineImage: Entity
{
    private ComplianceLineImage()
    {        
    }

    public Guid Id { get; private set; }
    public Guid ComplianceLineId { get; private set; }
    public string ImageName { get; private set; }

    internal static ComplianceLineImage Create(string imageName)
    {
        var image = new ComplianceLineImage
        {
            Id = Guid.NewGuid(),
            ImageName = imageName
        };

        return image;
    }
}
