using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Modules.OMS.Application.Webhooks.Choco;
using Crowbond.Modules.OMS.Domain.Webhooks.Choco;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Crowbond.Modules.OMS.Presentation.Webhooks;

internal sealed class ChocoOrderCreated: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("webhook/choco/order-created", async (HttpContext httpContext, IServiceProvider serviceProvider, ILogger<ChocoOrderCreated> logger) =>
        {
            try
            {
                // Read the raw request body
                string requestBody = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();
                
                // Extract the signature from the headers
                if (!httpContext.Request.Headers.TryGetValue("X-Choco-Signature", out StringValues signatureHeader))
                {
                    logger.LogWarning("Missing webhook signature");
                    return Results.Unauthorized();
                }

                string providedSignature = signatureHeader.ToString();

                // Validate the signature
                if (!IsValidSignature(requestBody, providedSignature))
                {
                    logger.LogWarning("Invalid webhook signature");
                    return Results.Unauthorized();
                }

                // Deserialize the payload
                ChocoOrderCreatedWebhook? payload = JsonSerializer.Deserialize<ChocoOrderCreatedWebhook>(requestBody);
                if (payload == null || payload.Payload?.Order == null)
                {
                    return Results.BadRequest("Invalid webhook payload.");
                }

                // Resolve command handler from DI
                ChocoOrderCreatedCommandHandler commandHandler = serviceProvider.GetRequiredService<ChocoOrderCreatedCommandHandler>();

                // Run command asynchronously
                _ = Task.Run(async () =>
                {
                    var command = new ChocoOrderCreatedCommand(payload);
                    await commandHandler.Handle(command, CancellationToken.None);
                });

                return Results.Ok(new { Message = "Webhook received successfully, processing asynchronously." });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing webhook: {ExMessage}", ex.Message);
                return Results.Problem($"An error occurred: {ex.Message}");
            }
        }).AllowAnonymous();
    }
    
    private const string WebhookSecret = "bb4a216378f1746a21fcffdf1f2f89ae30edb263a2763f2f1964e32a0209ddd2cc500ae976a0eec38f6a3fc0fd5bbcbe";
    
    private static bool IsValidSignature(string requestBody, string providedSignature)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(WebhookSecret));
        byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestBody));
        string computedSignature = Convert.ToBase64String(computedHash);
        return providedSignature.Equals(computedSignature, StringComparison.OrdinalIgnoreCase);
    }
}
