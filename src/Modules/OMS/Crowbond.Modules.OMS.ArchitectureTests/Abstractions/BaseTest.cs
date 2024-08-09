using System.Reflection;
using Crowbond.Modules.OMS.Application;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Infrastructure;

namespace Crowbond.Modules.OMS.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(OrderHeader).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(OmsModule).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(OMS.Presentation.AssemblyReference).Assembly;
}
