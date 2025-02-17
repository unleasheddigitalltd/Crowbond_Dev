using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Crowbond.Api.Options;

public class CorsOptionsSetup(IWebHostEnvironment environment, IConfiguration configuration, ILogger<CorsOptionsSetup> logger) : IConfigureOptions<CorsOptions>
{
    public void Configure(CorsOptions options)
    {        
         logger.LogInformation("Configuring CORS");
         logger.LogInformation("Environment: {Env}", environment.EnvironmentName);
    
        if(environment.IsDevelopment())
        {
           logger.LogInformation("Using development CORS policy");
       
            options.AddDefaultPolicy(policy => 
                policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        }
        else
        {
            logger.LogInformation("Using production CORS policy");
            var corsConfig = configuration.GetSection("Cors");
            var allowedOrigins = corsConfig.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
            var allowedMethods = corsConfig.GetSection("AllowedMethods").Get<string[]>() ?? new[] { "GET", "POST", "PUT", "DELETE", "OPTIONS" };
            var allowedHeaders = corsConfig.GetSection("AllowedHeaders").Get<string[]>() ?? new[] { "Authorization", "Content-Type", "Accept" };


            logger.LogInformation("Configuring CORS with allowed origins: {Origins}", string.Join(", ", allowedOrigins));
       
            options.AddDefaultPolicy(policy =>
                policy
                .WithOrigins(allowedOrigins)
                .WithMethods(allowedMethods)
                .WithHeaders(allowedHeaders)
                .AllowCredentials()
                .WithExposedHeaders("*"));
        }        
    }
}
