using Crowbond.Modules.OMS.Application.Choco.Requests;

namespace Crowbond.Modules.OMS.Application.Choco;

public interface IChocoClient
{
    public Task UpdateActionStatusAsync(UpdateActionStatusRequest request, CancellationToken ct);
}
