using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.CustomerProducts;

public static class CustomerProductErrors
{
    public static Error NotFound(Guid customerId, Guid prodictId) =>
    Error.NotFound("CustomerProducts.NotFound", $"The record for the customer id {customerId}, and the product id {prodictId} was not found");


    public static readonly Error InvalidTaxRateType =
        Error.Problem("CustomerProducts.InvalidTaxRateType", "The tax rate type is invalid");
    
}
