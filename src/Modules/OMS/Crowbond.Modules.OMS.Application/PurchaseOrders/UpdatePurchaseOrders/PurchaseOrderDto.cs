using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrder;
public sealed record PurchaseOrderDto(
    Guid? Id,
     string PurchaseOrderNo,
     string SupplierName,
     string ShippingAddressLine1,
     string? ShippingAddressLine2,
     string ShippingAddressTownCity,
     string ShippingAddressCounty,
     string? ShippingAddressCountry,
     string ShippingAddressPostalCode,
     string? PurchaseOrderNotes,
     string ExpectedDeliveryDate,
     string RequiredDate,
     string SupplierEmail,
     string SupplierPhone,
     string SupplierContact
    );
