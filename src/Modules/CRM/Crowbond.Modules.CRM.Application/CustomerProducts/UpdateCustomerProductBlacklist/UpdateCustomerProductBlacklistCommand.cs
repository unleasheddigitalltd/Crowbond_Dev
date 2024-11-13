using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.UpdateCustomerProductBlacklist;

public sealed record UpdateCustomerProductBlacklistCommand(Guid CustomerId, Guid ProductId, string? Comments) : ICommand;
