using Crowbond.Modules.OMS.Application.Choco.Enums;

namespace Crowbond.Modules.OMS.Application.Choco.Requests;

public class UpdateActionStatusRequest
{
    public string ActionId { get; set; } = string.Empty;
    public string ConnectionId { get; set; } = string.Empty;
    public string CorrelationId { get; set; } = string.Empty;
    public ChocoActionStatus Status { get; set; }
    public object Details { get; set; }
}
