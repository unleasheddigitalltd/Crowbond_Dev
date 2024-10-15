using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.CustomerProducts;
using Crowbond.Modules.OMS.Domain.Customers;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.Products;

namespace Crowbond.Modules.OMS.Application.Orders.SubstituteOrderLine;

internal sealed class SubstituteOrderLineCommandHandler(
    IOrderRepository orderRepository,
    InventoryService inventoryService,
    ICustomerProductApi customerProductApi,
    ICustomerApi customerApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<SubstituteOrderLineCommand, Guid>
{
    public async Task<Result<Guid>> Handle(SubstituteOrderLineCommand request, CancellationToken cancellationToken)
    {
        OrderLine? orderLine = await orderRepository.GetLineAsync(request.OrderLineId, cancellationToken);

        if (orderLine == null)
        {
            return Result.Failure<Guid>(OrderErrors.LineNotFound(request.OrderLineId));
        }

        decimal availableQty = await inventoryService.GetAvailableQuantityAsync(orderLine.ProductId, cancellationToken);

        if (availableQty >= orderLine.Qty)
        {
            return Result.Failure<Guid>(OrderErrors.NoShortage);
        }

        // Add substitute line.
        CustomerForOrderResponse? customer = await customerApi.GetAsync(orderLine.Header.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<Guid>(CustomerErrors.NotFound(orderLine.Header.CustomerId));
        }

        CustomerProductResponse? customerProduct = await customerProductApi.GetAsync(customer.Id, request.ProductId, cancellationToken);

        if (customerProduct is null)
        {
            return Result.Failure<Guid>(CustomerProductErrors.NotFound(customer.Id, request.ProductId));
        }

        if (!Enum.IsDefined(typeof(TaxRateType), customerProduct.TaxRateType))
        {
            return Result.Failure<Guid>(CustomerProductErrors.InvalidTaxRateType);
        }

        decimal unitPrice = (customer.NoDiscountFixedPrice && customerProduct.IsFixedPrice) ?
            customerProduct.UnitPrice :
            customerProduct.UnitPrice * ((100 - customer.Discount) / 100);

        Result<OrderLine> newOrderLineResult = orderLine.Header.AddLine(
            customerProduct.ProductId,
            customerProduct.ProductSku,
            customerProduct.ProductName,
            customerProduct.UnitOfMeasureName,
            customerProduct.CategoryId,
            customerProduct.CategoryName,
            customerProduct.BrandId,
            customerProduct.BrandName,
            customerProduct.ProductGroupId,
            customerProduct.ProductGroupName,
            unitPrice,
            orderLine.Qty,
            (TaxRateType)customerProduct.TaxRateType);

        if (newOrderLineResult.IsFailure)
        {
            return Result.Failure<Guid>(newOrderLineResult.Error);
        }

        orderRepository.AddLine(newOrderLineResult.Value);

        // remove current line qty.
        Result result = orderLine.Header.RemoveLine(orderLine.Id);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        orderRepository.RemoveLine(orderLine);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(newOrderLineResult.Value.Id);
    }
}
