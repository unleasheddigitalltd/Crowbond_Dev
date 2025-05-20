

namespace Crowbond.Modules.WMS.Domain.Reports
{
    public class ReportData
    {
        public string Title { get; set; }
        public List<ReportItem> Items { get; set; }
    }

    public class ReportItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
