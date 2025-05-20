

namespace Crowbond.Modules.WMS.Domain.Reports
{
    public interface IReportService
    {
        byte[] GenerateReport(ReportData data);
    }
}
