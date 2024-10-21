using System.Reflection;
using Crowbond.ArchitectureTests.Abstractions;
using Crowbond.Modules.CRM.Application.Customers.GetCustomers;
using Crowbond.Modules.CRM.Infrastructure;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Infrastructure;
using Crowbond.Modules.Users.Domain.Users;
using Crowbond.Modules.Users.Infrastructure;
using Crowbond.Modules.WMS.Application.Products.GetProducts;
using Crowbond.Modules.WMS.Infrastructure;
using NetArchTest.Rules;

namespace Crowbond.ArchitectureTests.Layers;

public class ModuleTests : BaseTest
{
    [Fact]
    public void UsersModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [WmsNamespace, OmsNamespace, CrmNamespace];
        string[] integrationEventsModules = [
            WmsIntegrationEventsNamespace,
            OmsIntegrationEventsNamespace,
            CrmIntegrationEventsNamespace];

        List<Assembly> usersAssemblies =
        [
            typeof(User).Assembly,
            Modules.Users.Application.AssemblyReference.Assembly,
            Modules.Users.Presentation.AssemblyReference.Assembly,
            typeof(UsersModule).Assembly
        ];

        Types.InAssemblies(usersAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void WmsModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [UsersNamespace, OmsNamespace, CrmNamespace];
        string[] integrationEventsModules = [
            UsersIntegrationEventsNamespace,
            OmsIntegrationEventsNamespace,
            CrmIntegrationEventsNamespace];

        List<Assembly> wmsAssemblies =
        [
            typeof(Product).Assembly,
            Modules.WMS.Application.AssemblyReference.Assembly,
            Modules.WMS.Presentation.AssemblyReference.Assembly,
            typeof(WmsModule).Assembly
        ];

        Types.InAssemblies(wmsAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void OmsModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [UsersNamespace, WmsNamespace, CrmNamespace];
        string[] integrationEventsModules = [
            UsersIntegrationEventsNamespace,
            WmsIntegrationEventsNamespace,
            CrmIntegrationEventsNamespace];

        List<Assembly> omsAssemblies =
        [
            typeof(OrderHeader).Assembly,
            Modules.OMS.Application.AssemblyReference.Assembly,
            Modules.OMS.Presentation.AssemblyReference.Assembly,
            typeof(OmsModule).Assembly
        ];

        Types.InAssemblies(omsAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void CrmModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [UsersNamespace, WmsNamespace, OmsNamespace];
        string[] integrationEventsModules = [
            UsersIntegrationEventsNamespace,
            WmsIntegrationEventsNamespace,
            OmsIntegrationEventsNamespace];

        List<Assembly> crmAssemblies =
        [
            typeof(Customer).Assembly,
            Modules.CRM.Application.AssemblyReference.Assembly,
            Modules.CRM.Presentation.AssemblyReference.Assembly,
            typeof(CrmModule).Assembly
        ];

        Types.InAssemblies(crmAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }
}
