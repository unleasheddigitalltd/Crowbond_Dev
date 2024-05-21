using System.Reflection;
using Crowbond.Modules.Ticketing.Application;
using Crowbond.Modules.Ticketing.Domain.Orders;
using Crowbond.Modules.Ticketing.Infrastructure;

namespace Crowbond.Modules.Ticketing.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Order).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(TicketingModule).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Ticketing.Presentation.AssemblyReference).Assembly;
}
