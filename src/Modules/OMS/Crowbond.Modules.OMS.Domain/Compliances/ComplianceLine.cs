using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Compliances;

public sealed class ComplianceLine: Entity
{
    private readonly List<ComplianceLineImage> _images = new();
    private ComplianceLine()
    {       
    }

    public Guid Id { get; private set; }
    public Guid ComplianceHeaderId { get; private set; }
    public Guid ComplianceQuestionId { get; private set; }
    public string? Description { get; private set; }
    public bool? Response { get; private set; }
    public IReadOnlyCollection<ComplianceLineImage> Images => _images;
    public ComplianceHeader Header { get; }

    internal static ComplianceLine Create(Guid questionId)
    {
        var line = new ComplianceLine
        {
            Id = Guid.NewGuid(),
            ComplianceQuestionId = questionId
        };

        return line;
    }

    internal void Update(bool? responce, string? description)
    {
        Response = responce;
        Description = description;
    }

    internal ComplianceLineImage AddImage(string imageName)
    {
        var image = ComplianceLineImage.Create(imageName);

        _images.Add(image);

        return image;
    }
    internal Result<ComplianceLineImage> RemoveImage(string imageName)
    {
        ComplianceLineImage? image = Images.SingleOrDefault(i => i.ImageName == imageName);

        if (image is null)
        {
            return Result.Failure<ComplianceLineImage>(ComplianceErrors.LineImageNotFound(imageName));
        }

        _images.Remove(image);

        return Result.Success(image);
    }
}
