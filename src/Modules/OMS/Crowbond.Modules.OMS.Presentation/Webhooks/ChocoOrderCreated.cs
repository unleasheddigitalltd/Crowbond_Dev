using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Modules.OMS.Application.Webhooks.Choco;
using Crowbond.Modules.OMS.Domain.Webhooks.Choco;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Crowbond.Modules.OMS.Presentation.Webhooks;

public sealed class ChocoOrderCreated : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("webhook/choco/order-created", HandleAsync).AllowAnonymous();
    }

    public async Task<IResult> HandleAsync(HttpContext httpContext,
        ISender sender,
        ILogger<ChocoOrderCreated> logger,
        IConfiguration configuration)
    {
        try
        {
            // Read the raw request body.
            using var reader = new StreamReader(httpContext.Request.Body);
            string requestBody = await reader.ReadToEndAsync();
            logger.LogInformation("Received webhook request: {RequestBody}", requestBody);

            // Verify the signature.
            if (!httpContext.Request.Headers.TryGetValue("X-Choco-Signature", out var signatureHeader))
            {
                logger.LogWarning("Missing webhook signature");
                return Results.Unauthorized();
            }
            string providedSignature = signatureHeader.ToString();
            if (!IsValidSignature(configuration, logger, requestBody, providedSignature))
            {
                logger.LogWarning("Invalid webhook signature");
                return Results.Unauthorized();
            }

            // Deserialize and validate payload.
            var payload = JsonSerializer.Deserialize<ChocoOrderCreatedWebhook>(requestBody);
            if (payload == null || payload.Payload?.Order == null)
            {
                return Results.BadRequest("Invalid webhook payload.");
            }

            await sender.Send(new ChocoOrderCreatedCommand(payload));

            return Results.Ok(new { Message = "Webhook received successfully, processing asynchronously." });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing webhook: {ExMessage}", ex.Message);
            return Results.Problem($"An error occurred: {ex.Message}");
        }
    }

    public static bool IsValidSignature(IConfiguration configuration, ILogger<ChocoOrderCreated> logger,
        string requestBody, string providedSignature)
    {
        string? webhookSecret = configuration["OMS:Choco:Auth:WebhookSecret"];
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
