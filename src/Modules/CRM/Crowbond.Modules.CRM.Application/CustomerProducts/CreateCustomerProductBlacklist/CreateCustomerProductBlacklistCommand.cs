using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.CreateCustomerProductBlacklist;

public sealed record CreateCustomerProductBlacklistCommand(Guid CustomerId, Guid ProductId, string? Comments) : ICommand;
