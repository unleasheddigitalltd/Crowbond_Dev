using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Reports.GetStockBuyersReport;

public sealed record GetStockBuyersReportQuery() : IQuery<StockBuyersReportResponse>;
