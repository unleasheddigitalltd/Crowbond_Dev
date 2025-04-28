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
    IDbConnectionFactory dbConnectionFactory, 
    HttpClient httpClient)
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

        // Step 2: Fetch delivery postcode coordinates
        string baseUrl = "https://api.postcodes.io/postcodes/";
        var deliveryResponse = await httpClient.GetStringAsync(baseUrl + request.postcode, cancellationToken);
        var deliveryData = JsonConvert.DeserializeObject<PostcodeApiResponse>(deliveryResponse);

        if (deliveryData?.Status != 200 || deliveryData.Result == null)
        {
            return Result.Failure<CustomerOutletForOrderResponse>(new Error("POSTCODE_NOT_FOUND", "Postcode not found.", ErrorType.Failure));
        }

        double deliveryLat = deliveryData.Result.Latitude;
        double deliveryLon = deliveryData.Result.Longitude;

        // Step 3: Bulk Lookup for Outlets
        var outletPostcodes = outlets.Select(o => o.PostalCode).Distinct().ToArray();
        var bulkRequestBody = JsonConvert.SerializeObject(new { postcodes = outletPostcodes });

        using var content = new StringContent(bulkRequestBody, Encoding.UTF8, "application/json");
        var bulkResponse = await httpClient.PostAsync($"{baseUrl}", content, cancellationToken);
        
        var bulkContent = await bulkResponse.Content.ReadAsStringAsync(cancellationToken);
        var bulkData = JsonConvert.DeserializeObject<BulkPostcodeApiResponse>(bulkContent);

        if (bulkData?.Status != 200 || bulkData.Result == null)
        {
            return Result.Failure<CustomerOutletForOrderResponse>(new Error("BULK_LOOKUP_FAILED", "Bulk lookup failed.", ErrorType.Failure));
        }

        var outletCoordinates = bulkData.Result
            .Where(r => r.Result != null)
            .ToDictionary(
                r => r.Query,
                r => new { r.Result.Latitude, r.Result.Longitude }
            );

        // Step 4: Find the Closest Outlet
        CustomerOutletForOrderResponse? closestOutlet = null;
        double minDistance = double.MaxValue;

        foreach (var outlet in outlets)
        {
            if (!outletCoordinates.TryGetValue(outlet.PostalCode, out var coords))
            {
                continue;
            }

            double distance = Haversine(deliveryLat, deliveryLon, coords.Latitude, coords.Longitude);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestOutlet = outlet;
            }
        }

        return closestOutlet ?? Result.Failure<CustomerOutletForOrderResponse>(new Error("OUTLET_NOT_FOUND", "Outlet not found.", ErrorType.Failure));
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
