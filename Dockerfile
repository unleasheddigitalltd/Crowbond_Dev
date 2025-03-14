FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH

ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Directory.Build.props", "."]

COPY ["src/API/Crowbond.Api/appsettings.json", "src/API/Crowbond.Api/"]
COPY ["src/API/Crowbond.Api/appsettings.*.json", "src/API/Crowbond.Api/"]


COPY ["src/API/Crowbond.Api/Crowbond.Api.csproj", "src/API/Crowbond.Api/"]
COPY ["src/Common/Crowbond.Common.Application/Crowbond.Common.Application.csproj", "src/Common/Crowbond.Common.Application/"]
COPY ["src/Common/Crowbond.Common.Domain/Crowbond.Common.Domain.csproj", "src/Common/Crowbond.Common.Domain/"]
COPY ["src/Common/Crowbond.Common.Infrastructure/Crowbond.Common.Infrastructure.csproj", "src/Common/Crowbond.Common.Infrastructure/"]
COPY ["src/Common/Crowbond.Common.Presentation/Crowbond.Common.Presentation.csproj", "src/Common/Crowbond.Common.Presentation/"]

COPY ["src/Modules/Users/Crowbond.Modules.Users.Application/Crowbond.Modules.Users.Application.csproj", "src/Modules/Users/Crowbond.Modules.Users.Application/"]
COPY ["src/Modules/Users/Crowbond.Modules.Users.Domain/Crowbond.Modules.Users.Domain.csproj", "src/Modules/Users/Crowbond.Modules.Users.Domain/"]
COPY ["src/Modules/Users/Crowbond.Modules.Users.Infrastructure/Crowbond.Modules.Users.Infrastructure.csproj", "src/Modules/Users/Crowbond.Modules.Users.Infrastructure/"]
COPY ["src/Modules/Users/Crowbond.Modules.Users.IntegrationEvents/Crowbond.Modules.Users.IntegrationEvents.csproj", "src/Modules/Users/Crowbond.Modules.Users.IntegrationEvents/"]
COPY ["src/Modules/Users/Crowbond.Modules.Users.Presentation/Crowbond.Modules.Users.Presentation.csproj", "src/Modules/Users/Crowbond.Modules.Users.Presentation/"]

COPY ["src/Modules/WMS/Crowbond.Modules.WMS.Application/Crowbond.Modules.WMS.Application.csproj", "src/Modules/WMS/Crowbond.Modules.WMS.Application/"]
COPY ["src/Modules/WMS/Crowbond.Modules.WMS.Domain/Crowbond.Modules.WMS.Domain.csproj", "src/Modules/WMS/Crowbond.Modules.WMS.Domain/"]
COPY ["src/Modules/WMS/Crowbond.Modules.WMS.Infrastructure/Crowbond.Modules.WMS.Infrastructure.csproj", "src/Modules/WMS/Crowbond.Modules.WMS.Infrastructure/"]
COPY ["src/Modules/WMS/Crowbond.Modules.WMS.IntegrationEvents/Crowbond.Modules.WMS.IntegrationEvents.csproj", "src/Modules/WMS/Crowbond.Modules.WMS.IntegrationEvents/"]
COPY ["src/Modules/WMS/Crowbond.Modules.WMS.Presentation/Crowbond.Modules.WMS.Presentation.csproj", "src/Modules/WMS/Crowbond.Modules.WMS.Presentation/"]
COPY ["src/Modules/WMS/Crowbond.Modules.WMS.PublicApi/Crowbond.Modules.WMS.PublicApi.csproj", "src/Modules/WMS/Crowbond.Modules.WMS.PublicApi/"]

COPY ["src/Modules/CRM/Crowbond.Modules.CRM.Application/Crowbond.Modules.CRM.Application.csproj", "src/Modules/CRM/Crowbond.Modules.CRM.Application/"]
COPY ["src/Modules/CRM/Crowbond.Modules.CRM.Domain/Crowbond.Modules.CRM.Domain.csproj", "src/Modules/CRM/Crowbond.Modules.CRM.Domain/"]
COPY ["src/Modules/CRM/Crowbond.Modules.CRM.Infrastructure/Crowbond.Modules.CRM.Infrastructure.csproj", "src/Modules/CRM/Crowbond.Modules.CRM.Infrastructure/"]
COPY ["src/Modules/CRM/Crowbond.Modules.CRM.IntegrationEvents/Crowbond.Modules.CRM.IntegrationEvents.csproj", "src/Modules/CRM/Crowbond.Modules.CRM.IntegrationEvents/"]
COPY ["src/Modules/CRM/Crowbond.Modules.CRM.Presentation/Crowbond.Modules.CRM.Presentation.csproj", "src/Modules/CRM/Crowbond.Modules.CRM.Presentation/"]
COPY ["src/Modules/CRM/Crowbond.Modules.CRM.PublicApi/Crowbond.Modules.CRM.PublicApi.csproj", "src/Modules/CRM/Crowbond.Modules.CRM.PublicApi/"]

COPY ["src/Modules/OMS/Crowbond.Modules.OMS.Application/Crowbond.Modules.OMS.Application.csproj", "src/Modules/OMS/Crowbond.Modules.OMS.Application/"]
COPY ["src/Modules/OMS/Crowbond.Modules.OMS.Domain/Crowbond.Modules.OMS.Domain.csproj", "src/Modules/OMS/Crowbond.Modules.OMS.Domain/"]
COPY ["src/Modules/OMS/Crowbond.Modules.OMS.Infrastructure/Crowbond.Modules.OMS.Infrastructure.csproj", "src/Modules/OMS/Crowbond.Modules.OMS.Infrastructure/"]
COPY ["src/Modules/OMS/Crowbond.Modules.OMS.IntegrationEvents/Crowbond.Modules.OMS.IntegrationEvents.csproj", "src/Modules/OMS/Crowbond.Modules.OMS.IntegrationEvents/"]
COPY ["src/Modules/OMS/Crowbond.Modules.OMS.Presentation/Crowbond.Modules.OMS.Presentation.csproj", "src/Modules/OMS/Crowbond.Modules.OMS.Presentation/"]
COPY ["src/Modules/OMS/Crowbond.Modules.OMS.PublicApi/Crowbond.Modules.OMS.PublicApi.csproj", "src/Modules/OMS/Crowbond.Modules.OMS.PublicApi/"]

RUN dotnet restore "./src/API/Crowbond.Api/Crowbond.Api.csproj"
COPY . .
WORKDIR "/src/src/API/Crowbond.Api"
RUN dotnet build "./Crowbond.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Crowbond.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Crowbond.Api.dll"]