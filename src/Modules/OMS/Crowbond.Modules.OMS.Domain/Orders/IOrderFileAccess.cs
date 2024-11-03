using Microsoft.AspNetCore.Http;

namespace Crowbond.Modules.OMS.Domain.Orders;

public interface IOrderFileAccess
{
    public Task<List<string>> SaveDeliveryImagesAsync(string orderNo, int lastSequence, IFormFileCollection images, CancellationToken cancellationToken = default);

    public Task DeleteDeliveryImageAsync(string imageName, CancellationToken cancellationToken = default);
}
