using Crowbond.Modules.WMS.Domain.Receipts;

namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptHeaderDetails;

public sealed record ReceiptHeaderDetailsResponse(
    Guid Id,
    string ReceiptNo,    
    DateOnly? ReceivedDate,    
    Guid? PurchaseOrderId,    
    string? PurchaseOrderNo,    
    string? DeliveryNoteNumber,    
    Guid? LocationId,    
    ReceiptStatus Status);
