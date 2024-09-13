using System;
using Crowbond.Common.Application.Messaging;
namespace Crowbond.Modules.CRM.Application.ProductPrices.UpdatePriceTierPrices;

public sealed record UpdatePriceTierPricesCommand(Guid PriceTierId, IReadOnlyCollection<ProductPriceRequest> ProductPrices) : ICommand;
