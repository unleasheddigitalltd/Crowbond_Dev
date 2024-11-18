using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.DeleteCustomerProduct;

public sealed record DeleteCustomerProductCommand(Guid CustomerId, Guid ProductId) : ICommand;
