﻿using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.Suppliers.UpdateSupplierActivation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.Suppliers;

internal sealed class UpdateSupplierActivation : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/suppliers/{id}/activation/{isActive}", async (Guid id, bool isActive, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateSupplierActivationCommand(id, isActive));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        }
        )
        .RequireAuthorization(Permissions.ModifySuppliers)
        .WithTags(Tags.Suppliers);
    }
}
