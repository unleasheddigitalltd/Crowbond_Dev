using Microsoft.AspNetCore.Http;

namespace Crowbond.Modules.OMS.Domain.Orders;

public interface IOrderFileAccess
{
    public Task<string> SaveDeliveryImageAsync(string orderNo, int LastSequence, IFormFile image, CancellationToken cancellationToken = default);

    public Task DeleteDeliveryImageAsync(string imageName, CancellationToken cancellationToken = default);
}
