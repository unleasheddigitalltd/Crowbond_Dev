using Microsoft.AspNetCore.Http;

namespace Crowbond.Modules.OMS.Domain.Compliances;

public interface IComplianceFileAccess
{
    public Task<List<string>> SaveLineImagesAsync(string formNo, int lastSequence, IFormFileCollection images, CancellationToken cancellationToken = default);

    public Task DeleteLineImageAsync(string imageName, CancellationToken cancellationToken = default);
}
