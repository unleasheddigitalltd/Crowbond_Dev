using System.Reflection;
using Crowbond.Modules.WMS.Application;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Infrastructure;

namespace Crowbond.Modules.WMS.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(ReceiptHeader).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(WmsModule).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(WMS.Presentation.AssemblyReference).Assembly;
}
