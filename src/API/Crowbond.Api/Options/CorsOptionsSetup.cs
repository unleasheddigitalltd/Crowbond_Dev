using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

namespace Crowbond.Api.Options;

public class CorsOptionsSetup(IWebHostEnvironment environment) : IConfigureOptions<CorsOptions>
{
    public void Configure(CorsOptions options)
    {        
        
        if(environment.IsDevelopment())
        {
            options.AddDefaultPolicy(policy => 
                policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        }
        else
        {
            options.AddDefaultPolicy(policy =>
                policy
                .WithOrigins()
                .WithMethods()
                .WithHeaders());
        }        
    }
}
