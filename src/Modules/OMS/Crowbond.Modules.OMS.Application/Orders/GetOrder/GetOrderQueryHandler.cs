using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrder;

internal sealed class GetOrderQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetOrderQueryHandler, OrderResponse>
{
    public async Task<Result<OrderResponse>> Handle(GetOrderQueryHandler request, CancellationToken cancellationToken)
    {
        
    }
}
