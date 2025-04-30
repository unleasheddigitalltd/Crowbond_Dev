using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Crowbond.Modules.OMS.Application.Choco.Requests;
using Crowbond.Modules.OMS.Application.Choco.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Crowbond.Modules.OMS.Application.Choco
{
    public class ChocoClient : IChocoClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ChocoClient> _logger;

        public ChocoClient(HttpClient httpClient, IConfiguration configuration, ILogger<ChocoClient> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task UpdateActionStatusAsync(UpdateActionStatusRequest request, CancellationToken ct)
        {
            // 1) Get fresh bearer token
            var token = await AuthenticateAsync(ct);

            // 2) Prepare headers
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _configuration["OMS:Choco:Auth:ApiKey"]);
            _httpClient.DefaultRequestHeaders.Add("x-connection-id", _configuration["OMS:Choco:Auth:ConnectionId"]);

            // 3) Build the body
            var payload = new
            {
                status = request.Status.ToString(),
                details = request.Details ?? new { }
            };

            // 4) Send PATCH
            var endpoint = $"adapter/v1/actions/{request.ActionId}";
            using var httpReq = new HttpRequestMessage(new HttpMethod("PATCH"), endpoint);
            httpReq.Content = JsonContent.Create(payload, mediaType: null,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var resp = await _httpClient.SendAsync(httpReq, ct);

            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError(
                    "Choco UpdateActionStatus failed. Status {StatusCode}, Body: {Body}",
                    resp.StatusCode, body);
                // you can choose to throw or swallow based on your retry policy
            }
        }

        private async Task<string> AuthenticateAsync(CancellationToken ct)
        {
            var authUrl      = _configuration["OMS:Choco:AuthUrl"]
                               ?? throw new InvalidOperationException("Missing configuration for OMS:Choco:AuthUrl");
            var clientId     = _configuration["OMS:Choco:Auth:ClientId"]
                               ?? throw new InvalidOperationException("Missing configuration for OMS:Choco:Auth:ClientId");
            var clientSecret = _configuration["OMS:Choco:Auth:ClientSecret"]
                               ?? throw new InvalidOperationException("Missing configuration for OMS:Choco:Auth:ClientSecret");

            var creds = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

            using var authReq = new HttpRequestMessage(HttpMethod.Post, $"{authUrl}/oauth2/token");
            authReq.Headers.Authorization = new AuthenticationHeaderValue("Basic", creds);
            authReq.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["scope"] = "openid adapter.choco/actions/update"
            });

            var resp = await _httpClient.SendAsync(authReq, ct);
            resp.EnsureSuccessStatusCode();

            var tokenResp = await resp.Content.ReadFromJsonAsync<ChocoTokenResponse>(cancellationToken: ct)
                            ?? throw new InvalidOperationException("Invalid Choco token response");
            return tokenResp.AccessToken;
        }
    }
}
