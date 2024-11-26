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

        OrderHeader? order = await orderRepository.GetAsync(orderLine.OrderHeaderId, cancellationToken);

        if (order is null)
        {
            return Result.Failure<Guid>(OrderErrors.NotFound(orderLine.OrderHeaderId));
        }

        decimal availableQty = await inventoryService.GetAvailableQuantityAsync(orderLine.ProductId, cancellationToken);

        if (availableQty >= orderLine.OrderedQty)
        {
            return Result.Failure<Guid>(OrderErrors.NoShortage);
        }

        // Add substitute line.
        CustomerForOrderResponse? customer = await customerApi.GetAsync(order.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<Guid>(CustomerErrors.NotFound(order.CustomerId));
        }

        CustomerProductResponse? customerProduct = await customerProductApi.GetAsync(customer.Id, request.ProductId, cancellationToken);

        if (customerProduct is null)
        {
            return Result.Failure<Guid>(CustomerProductErrors.NotFound(customer.Id, request.ProductId));
        }

        if (customerProduct.IsBlacklisted)
        {
            return Result.Failure<Guid>(OrderErrors.ProductIsBlacklisted(request.ProductId));
        }

        if (!Enum.IsDefined(typeof(TaxRateType), customerProduct.TaxRateType))
        {
            return Result.Failure<Guid>(CustomerProductErrors.InvalidTaxRateType);
        }

        Result<OrderLine> newOrderLineResult = order.AddLine(
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
            customerProduct.UnitPrice,
            request.Qty,
            (TaxRateType)customerProduct.TaxRateType);

        if (newOrderLineResult.IsFailure)
        {
            return Result.Failure<Guid>(newOrderLineResult.Error);
        }

        orderRepository.AddLine(newOrderLineResult.Value);

        // remove current line qty.
        Result result = order.RemoveLine(orderLine.Id);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        orderRepository.RemoveLine(orderLine);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(newOrderLineResult.Value.Id);
    }
}
