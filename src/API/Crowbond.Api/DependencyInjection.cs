using System.Reflection;
using Crowbond.Api.Extensions;
using Crowbond.Api.Middleware;
using Crowbond.Common.Infrastructure;
using Crowbond.Common.Infrastructure.Configuration;
using Crowbond.Modules.Users.Infrastructure;
using Crowbond.Modules.WMS.Infrastructure;
using Crowbond.Modules.CRM.Infrastructure;
using Crowbond.Modules.OMS.Infrastructure;
using Crowbond.Common.Application;
using Crowbond.Api.Options;

namespace Crowbond.Api;

public static class DependencyInjection
{
    public static void AddServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocumentation();

        Assembly[] moduleApplicationAssemblies = [
            Modules.Users.Application.AssemblyReference.Assembly,
            Modules.WMS.Application.AssemblyReference.Assembly,
            Modules.CRM.Application.AssemblyReference.Assembly,
            Modules.OMS.Application.AssemblyReference.Assembly];

        services.AddApplication(moduleApplicationAssemblies);

        string databaseConnectionString = configuration.GetConnectionStringOrThrow("Database");

        services.AddInfrastructure(
            [
                UsersModule.ConfigureConsumers,
                WmsModule.ConfigureConsumers,
                OmsModule.ConfigureConsumers,
                CrmModule.ConfigureConsumers,
            ],
            databaseConnectionString);
        
        services.AddHealthChecks()
            .AddNpgSql(databaseConnectionString);
            
        services.AddUsersModule(configuration);
        services.AddWMSModule(configuration);
        services.AddCRMModule(configuration);
        services.AddOMSModule(configuration);

        services.ConfigureOptions<CorsOptionsSetup>();
        services.AddCors();

        services.ConfigureOptions<AntiForgeryOptionsSetup>();
        services.AddAntiforgery();
    }
}
