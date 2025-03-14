using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Amazon.CognitoIdentityProvider;
using Crowbond.Modules.Users.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Crowbond.Modules.Users.Infrastructure.Authentication;

public class CognitoTokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly CognitoOptions _options;

    public CognitoTokenValidationMiddleware(
        RequestDelegate next,
        IOptions<CognitoOptions> options)
    {
        _next = next;
        _options = options.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string? authHeader = context.Request.Headers.Authorization.FirstOrDefault();

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.Ordinal))
        {
            await _next(context);
            return;
        }

        string token = authHeader.Substring("Bearer ".Length);

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Validate token hasn't expired
            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { error = "Token has expired" });
                return;
            }

            // Validate issuer
            string cognitoIssuer = $"https://cognito-idp.{_options.Region}.amazonaws.com/{_options.UserPoolId}";
            if (jwtToken.Issuer != cognitoIssuer)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { error = "Invalid token issuer" });
                return;
            }

            // Add claims to the current user
            var identity = new ClaimsIdentity(jwtToken.Claims, JwtBearerDefaults.AuthenticationScheme);
            context.User = new ClaimsPrincipal(identity);

            await _next(context);
        }
        catch (Exception)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { error = "Invalid token" });
        }
    }
}
