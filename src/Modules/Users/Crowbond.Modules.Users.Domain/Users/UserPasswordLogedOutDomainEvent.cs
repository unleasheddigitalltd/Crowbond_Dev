using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crowbond.Common.Domain;

namespace Crowbond.Modules.Users.Domain.Users;
public sealed class UserPasswordLogedOutDomainEvent(Guid eventId) : DomainEvent
{
    public Guid UserId { get; init; } = eventId;
}
