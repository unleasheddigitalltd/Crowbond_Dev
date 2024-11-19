using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.CustomerProducts;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.Products;

namespace Crowbond.Modules.OMS.Application.Orders.AddOrderLine;

internal sealed class AddOrderLineCommandHandler(
    ICustomerProductApi customerProductApi,
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddOrderLineCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AddOrderLineCommand request, CancellationToken cancellationToken)
    {
        OrderHeader? orderHeader = await orderRepository.GetAsync(request.OrderHeaderId, cancellationToken);

        if (orderHeader is null)
        {
            return Result.Failure<Guid>(OrderErrors.NotFound(request.OrderHeaderId));
        }

        CustomerProductResponse? customerProduct = await customerProductApi.GetAsync(orderHeader.CustomerId, request.ProductId, cancellationToken);

        if (customerProduct is null)
        {
            return Result.Failure<Guid>(CustomerProductErrors.NotFound(orderHeader.CustomerId, request.ProductId));
        }

        if (!Enum.IsDefined(typeof(TaxRateType), customerProduct.TaxRateType))
        {
            return Result.Failure<Guid>(CustomerProductErrors.InvalidTaxRateType);
        }


        OrderLine? existingLine = orderHeader.Lines.SingleOrDefault(l => l.ProductId == request.ProductId);

        if (existingLine != null)
        {
            return Result.Failure<Guid>(OrderErrors.LineForProductExists(request.ProductId));
        }

        Result<OrderLine> result = orderHeader.AddLine(
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

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }
        orderRepository.AddLine(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(result.Value.Id);
    }
}
