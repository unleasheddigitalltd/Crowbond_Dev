using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.Settings.UpdateSettings;
using Crowbond.Modules.CRM.Domain.Settings;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.Settings;

internal sealed class UpdateSettings : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("crm/settings", async (Request request, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateSettingsCommand(request.PaymentTerms));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifySettings)
            .WithTags(Tags.Settings);
            
    }

    private sealed record Request(PaymentTerms? PaymentTerms);
}
