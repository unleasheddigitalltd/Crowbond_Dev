using System.Data.Common;
using System.Text;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutletForOrder;
using Dapper;
using Newtonsoft.Json;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutletForOrderByPostcode;

internal sealed class GetCustomerOutletForOrderByPostcodeQueryHandler(
    IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerOutletForOrderByPostcodeQuery, CustomerOutletForOrderResponse>
{
    public async Task<Result<CustomerOutletForOrderResponse>> Handle(GetCustomerOutletForOrderByPostcodeQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        // Step 1: Fetch active outlets
        const string sql = """
            SELECT 
                id AS Id, postal_code AS PostalCode
            FROM crm.customer_outlets
            WHERE customer_id = @CustomerID 
              AND is_deleted = false 
              AND is_active = true
        """;

        var outlets = (await connection.QueryAsync<CustomerOutletForOrderResponse>(sql, new { CustomerId = request.customerId })).ToList();
        if (!outlets.Any())
        {
            return Result.Failure<CustomerOutletForOrderResponse>(new Error("NO_OUTLET", "No active outlets found.", ErrorType.Failure));
        }

        return outlets.FirstOrDefault();
    }

    private static double Haversine(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371; // Earth radius in km
        double dLat = (lat2 - lat1) * (Math.PI / 180);
        double dLon = (lon2 - lon1) * (Math.PI / 180);
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(lat1 * (Math.PI / 180)) * Math.Cos(lat2 * (Math.PI / 180)) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }
}

// Response Models
public class PostcodeApiResponse
{
    public int Status { get; set; }
    public PostcodeResult? Result { get; set; }
}

public class PostcodeResult
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class BulkPostcodeApiResponse
{
    public int Status { get; set; }
    public List<BulkPostcodeResult>? Result { get; set; }
}

public class BulkPostcodeResult
{
    public string Query { get; set; } = string.Empty;
    public PostcodeResult Result { get; set; }
}
