using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Orders.GetOrder;
using Dapper;

namespace Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripsByStatus;

internal sealed class GetRouteTripsByStatusQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRouteTripsByStatusQuery, IReadOnlyCollection<RouteTripByStatusResponse>>
{

 public async Task<Result<IReadOnlyCollection<RouteTripByStatusResponse>>> Handle(GetRouteTripsByStatusQuery request, CancellationToken cancellationToken)
{
    await using var connection = await dbConnectionFactory.OpenConnectionAsync();

    var routeTrips = await GetRouteTripsByStatus(connection, (int)request.Status);
    
    foreach (var routeTrip in routeTrips)
    {
        var orders = await GetOrdersByRouteTripId(connection, routeTrip.Id);
        routeTrip.Orders.AddRange(orders);
    }
    
    return routeTrips;
}

private static async Task<List<RouteTripByStatusResponse>> GetRouteTripsByStatus(DbConnection connection, int status)
{
    const string sql = @"
        SELECT 
            t.id AS Id,
            r.name AS RouteName,
            r.position AS Position,
            t.status AS Status,
            t.date AS Date,
            t.comments AS Comments
        FROM oms.route_trips t
        INNER JOIN oms.routes r ON r.id = t.route_id
        WHERE t.status = @Status
        ORDER BY r.position, t.date";
    
    var dto = await connection.QueryAsync<RouteTripDto>(sql, new { Status = status });
    
    
    return dto.Select(rt => new RouteTripByStatusResponse(
        rt.Id,
        rt.RouteName,
        rt.Position,
        rt.Status,
        rt.Date,
        rt.Comments,
        []
    )).ToList();
}

private async Task<List<OrderResponse>> GetOrdersByRouteTripId(DbConnection connection, Guid routeTripId)
{
    const string sql = @"
        SELECT 
            o.id AS Id,
            o.customer_id AS CustomerId,
            o.order_no as OrderNo,
            o.customer_account_number AS CustomerAccountNumber,
            o.customer_business_name AS CustomerBusinessName,
            o.shipping_date as ShippingDate,
            o.status AS Status,
            o.delivery_charge as DeliveryCharge,
            o.order_amount as OrderAmount
        FROM oms.order_headers o
        WHERE o.route_trip_id = @RouteTripId";
    
    return (await connection.QueryAsync<OrderDto>(sql, new { RouteTripId = routeTripId })).Select(o => new OrderResponse(
        o.Id,
        o.CustomerId,
        o.OrderNo,
        o.CustomerAccountNumber,
        o.CustomerBusinessName,
        o.ShippingDate,
        o.Status,
        o.DeliveryCharge,
        o.OrderAmount
    )).ToList();
}


    [SuppressMessage("SonarAnalyzer.CSharp", "S3459")]
    private sealed class RouteTripDto
    {
        public Guid Id { get; set; }
        public string RouteName { get; set; } = string.Empty;
        public int Position { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public string? Comments { get; set; }
    }
    
    [SuppressMessage("SonarAnalyzer.CSharp", "S3459")]
    private sealed class OrderDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string OrderNo { get; set; } = string.Empty;
        public string CustomerAccountNumber { get; set; } = string.Empty;
        public string CustomerBusinessName { get; set; } = string.Empty;
        public DateTime ShippingDate { get; set; }
        public int Status { get; set; }
        public decimal DeliveryCharge { get; set; }
        public decimal OrderAmount { get; set; }
    }

}


