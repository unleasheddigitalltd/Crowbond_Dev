using System.Reflection;
using Crowbond.Modules.Events.Application;
using Crowbond.Modules.Events.Domain.Events;
using Crowbond.Modules.Events.Infrastructure;

namespace Crowbond.Modules.Events.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Event).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(EventsModule).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Events.Presentation.AssemblyReference).Assembly;
}
