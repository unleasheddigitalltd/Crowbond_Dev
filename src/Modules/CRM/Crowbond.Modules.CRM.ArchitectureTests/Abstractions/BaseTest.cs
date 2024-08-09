using System.Reflection;
using Crowbond.Modules.CRM.Application;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Infrastructure;

namespace Crowbond.Modules.CRM.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Customer).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(CrmModule).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(CRM.Presentation.AssemblyReference).Assembly;
}

