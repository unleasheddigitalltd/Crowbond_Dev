using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.ProductPrices.UpdateProductPrices;

public sealed record UpdateProductPricesCommand(Guid ProductId, IReadOnlyCollection<ProductPriceRequest> ProductPrices) : ICommand;
