using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.DeleteCustomerProductBlacklist;

public sealed record DeleteCustomerProductBlacklistCommand(Guid CustomerId, Guid ProductId) : ICommand;
