using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrderDeliveryImages;

internal sealed class GetOrderDeliveryImagesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetOrderDeliveryImagesQuery, IReadOnlyCollection<DeliveryImageResponse>>
{
    public async Task<Result<IReadOnlyCollection<DeliveryImageResponse>>> Handle(GetOrderDeliveryImagesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 di.id AS {nameof(DeliveryImageResponse.Id)},
                 di.image_name AS {nameof(DeliveryImageResponse.ImageName)}
             FROM oms.order_delivery_images di
             INNER JOIN oms.order_deliveries d ON d.id = di.order_delivery_id
             INNER JOIN oms.order_headers o ON o.id = d.order_header_id
             WHERE o.id = @OrderHeaderId
             """;

        List<DeliveryImageResponse> deliveryImages = (await connection.QueryAsync<DeliveryImageResponse>(sql, request)).AsList();

        return deliveryImages;
    }
}
