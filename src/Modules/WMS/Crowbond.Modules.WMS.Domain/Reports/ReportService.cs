using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

using System.Globalization;

namespace Crowbond.Modules.WMS.Domain.Reports
{
    public class ReportService : IReportService
    {
        public byte[] GenerateReport(ReportData data)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text(data.Title).FontSize(20).Bold();
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.ConstantColumn(100);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Item");
                            header.Cell().Text("Quantity");
                        });

                        foreach (var item in data.Items)
                        {
                            table.Cell().Text(item.Name);
                            table.Cell().Text(item.Quantity.ToString(CultureInfo.InvariantCulture));
                        }
                    });
                    page.Footer().AlignCenter().Text(x => x.Span("Generated on "));
                });
            });

            return document.GeneratePdf();
        }
    }
}
