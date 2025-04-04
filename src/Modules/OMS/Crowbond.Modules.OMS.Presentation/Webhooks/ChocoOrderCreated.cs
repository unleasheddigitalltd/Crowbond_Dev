using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Modules.OMS.Application.Webhooks.Choco;
using Crowbond.Modules.OMS.Domain.Webhooks.Choco;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Crowbond.Modules.OMS.Presentation.Webhooks;

internal sealed class ChocoOrderCreated : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("webhook/choco/order-created", async (HttpContext httpContext, 
            IServiceProvider serviceProvider,
            ILogger<ChocoOrderCreated> logger,
            IConfiguration configuration) =>
        {
            try
            {
                // Read the raw request body
                string requestBody = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();
                
                logger.LogInformation("Received webhook request: {RequestBody}", requestBody);
                
                // Extract the signature from the headers
                if (!httpContext.Request.Headers.TryGetValue("X-Choco-Signature", out StringValues signatureHeader))
                {
                    logger.LogWarning("Missing webhook signature");
                    return Results.Unauthorized();
                }

                string providedSignature = signatureHeader.ToString();

                // Validate the signature
                if (!IsValidSignature(configuration, logger, requestBody, providedSignature))
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
                ChocoOrderCreatedCommandHandler commandHandler =
                    serviceProvider.GetRequiredService<ChocoOrderCreatedCommandHandler>();

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

    private static bool IsValidSignature(IConfiguration configuration, ILogger<ChocoOrderCreated> logger,
        string requestBody, string providedSignature)
    {
        string? webhookSecret = configuration["OMS:Choco:Secret"];
    
        if (string.IsNullOrWhiteSpace(webhookSecret))
        {
            logger.LogWarning("Webhook secret is missing");
            return false;
        }

        providedSignature = providedSignature.Trim();

        byte[] keyBytes = Encoding.UTF8.GetBytes(webhookSecret);
        byte[] bodyBytes = Encoding.UTF8.GetBytes(requestBody);

        using var hmac = new HMACSHA256(keyBytes);
        byte[] computedHash = hmac.ComputeHash(bodyBytes);
        
        string computedSignature = Convert.ToHexString(computedHash);

        logger.LogInformation("Computed Signature: {ComputedSignature}, Provided Signature: {ProvidedSignature}",
            computedSignature, providedSignature);

        return providedSignature.Equals(computedSignature, StringComparison.OrdinalIgnoreCase);
    }
}
