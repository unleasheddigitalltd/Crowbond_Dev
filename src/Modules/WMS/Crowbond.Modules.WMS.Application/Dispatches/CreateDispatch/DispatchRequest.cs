namespace Crowbond.Modules.WMS.Application.Dispatches.CreateDispatch;

public sealed record DispatchRequest(
    Guid OrderId,
    string OrderNo)
{
    public List<DispatchLineRequest> Lines { get; set; }
}
