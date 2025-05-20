using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Domain.Reports;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using QuestPDF.Infrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static MassTransit.ValidationResultExtensions;
using QuestPDF.Fluent;

namespace Crowbond.Modules.WMS.Presentation.Reports;


internal sealed class GetReport : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/report/generate", ([FromServices] IReportService reportService) =>
        {

            try
            {
               var data = new ReportData
                {
                    Title = "Inventory Report",
                    Items = new List<ReportItem>
                    {
                        new ReportItem { Name = "Eggs", Quantity = 120 },
                        new ReportItem { Name = "Milk", Quantity = 45 }
                    }
               };

                var pdfBytes = reportService.GenerateReport(data);

                // ✅ This should return with content-type: application/pdf
                return Results.File(pdfBytes, "application/pdf", "report.pdf");
            }
            catch (Exception ex)
            {
                return Results.Problem(title: "Error generating PDF", detail: ex.Message);
            }
        })
        .WithName("GenerateReport")
        .Produces<FileContentResult>(200, "application/pdf")
        .WithTags("Reports");
    }
}



