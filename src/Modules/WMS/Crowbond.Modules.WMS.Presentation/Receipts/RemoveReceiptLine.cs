﻿using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Receipts.RemoveReceiptLine;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Receipts;

internal sealed class RemoveReceiptLine : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("receipts/lines/{id}", async (Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new RemoveReceiptLineCommand(id));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyReceipts)
            .WithTags(Tags.Receipts);
    }
}
