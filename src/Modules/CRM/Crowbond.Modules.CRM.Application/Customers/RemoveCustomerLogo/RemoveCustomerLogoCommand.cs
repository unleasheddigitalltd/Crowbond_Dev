using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Customers.RemoveCustomerLogo;

public sealed record RemoveCustomerLogoCommand(Guid UserId, Guid CustomerId) : ICommand;
