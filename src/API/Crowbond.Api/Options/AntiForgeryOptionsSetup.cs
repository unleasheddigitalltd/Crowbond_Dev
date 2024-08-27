using Microsoft.AspNetCore.Antiforgery;
using Microsoft.Extensions.Options;

namespace Crowbond.Api.Options;

public class AntiForgeryOptionsSetup : IConfigureOptions<AntiforgeryOptions>
{
    public void Configure(AntiforgeryOptions options)
    {
        options.HeaderName = "X-XSRF-TOKEN";
    }    
}
