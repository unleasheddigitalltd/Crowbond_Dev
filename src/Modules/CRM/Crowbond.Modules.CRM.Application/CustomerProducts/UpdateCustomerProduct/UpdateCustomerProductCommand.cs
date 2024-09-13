using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.UpdateCustomerProduct;

public sealed record UpdateCustomerProductCommand(Guid CustomerId, IReadOnlyCollection<CustomerProductRequest> CustomerProducts) : ICommand;
