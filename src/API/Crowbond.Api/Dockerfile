FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Directory.Build.props", "."]

COPY ["src/API/Crowbond.Api/Crowbond.Api.csproj", "src/API/Crowbond.Api/"]
COPY ["src/Common/Crowbond.Common.Application/Crowbond.Common.Application.csproj", "src/Common/Crowbond.Common.Application/"]
COPY ["src/Common/Crowbond.Common.Domain/Crowbond.Common.Domain.csproj", "src/Common/Crowbond.Common.Domain/"]
COPY ["src/Common/Crowbond.Common.Infrastructure/Crowbond.Common.Infrastructure.csproj", "src/Common/Crowbond.Common.Infrastructure/"]
COPY ["src/Common/Crowbond.Common.Presentation/Crowbond.Common.Presentation.csproj", "src/Common/Crowbond.Common.Presentation/"]

COPY ["src/Modules/Attendance/Crowbond.Modules.Attendance.Application/Crowbond.Modules.Attendance.Application.csproj", "src/Modules/Attendance/Crowbond.Modules.Attendance.Application/"]
COPY ["src/Modules/Attendance/Crowbond.Modules.Attendance.Domain/Crowbond.Modules.Attendance.Domain.csproj", "src/Modules/Attendance/Crowbond.Modules.Attendance.Domain/"]
COPY ["src/Modules/Attendance/Crowbond.Modules.Attendance.Infrastructure/Crowbond.Modules.Attendance.Infrastructure.csproj", "src/Modules/Attendance/Crowbond.Modules.Attendance.Infrastructure/"]
COPY ["src/Modules/Attendance/Crowbond.Modules.Attendance.IntegrationEvents/Crowbond.Modules.Attendance.IntegrationEvents.csproj", "src/Modules/Attendance/Crowbond.Modules.Attendance.IntegrationEvents/"]
COPY ["src/Modules/Attendance/Crowbond.Modules.Attendance.Presentation/Crowbond.Modules.Attendance.Presentation.csproj", "src/Modules/Attendance/Crowbond.Modules.Attendance.Presentation/"]

COPY ["src/Modules/Events/Crowbond.Modules.Events.Application/Crowbond.Modules.Events.Application.csproj", "src/Modules/Events/Crowbond.Modules.Events.Application/"]
COPY ["src/Modules/Events/Crowbond.Modules.Events.Domain/Crowbond.Modules.Events.Domain.csproj", "src/Modules/Events/Crowbond.Modules.Events.Domain/"]
COPY ["src/Modules/Events/Crowbond.Modules.Events.Infrastructure/Crowbond.Modules.Events.Infrastructure.csproj", "src/Modules/Events/Crowbond.Modules.Events.Infrastructure/"]
COPY ["src/Modules/Events/Crowbond.Modules.Events.IntegrationEvents/Crowbond.Modules.Events.IntegrationEvents.csproj", "src/Modules/Events/Crowbond.Modules.Events.IntegrationEvents/"]
COPY ["src/Modules/Events/Crowbond.Modules.Events.Presentation/Crowbond.Modules.Events.Presentation.csproj", "src/Modules/Events/Crowbond.Modules.Events.Presentation/"]

COPY ["src/Modules/Ticketing/Crowbond.Modules.Ticketing.Application/Crowbond.Modules.Ticketing.Application.csproj", "src/Modules/Ticketing/Crowbond.Modules.Ticketing.Application/"]
COPY ["src/Modules/Ticketing/Crowbond.Modules.Ticketing.Domain/Crowbond.Modules.Ticketing.Domain.csproj", "src/Modules/Ticketing/Crowbond.Modules.Ticketing.Domain/"]
COPY ["src/Modules/Ticketing/Crowbond.Modules.Ticketing.Infrastructure/Crowbond.Modules.Ticketing.Infrastructure.csproj", "src/Modules/Ticketing/Crowbond.Modules.Ticketing.Infrastructure/"]
COPY ["src/Modules/Ticketing/Crowbond.Modules.Ticketing.IntegrationEvents/Crowbond.Modules.Ticketing.IntegrationEvents.csproj", "src/Modules/Ticketing/Crowbond.Modules.Ticketing.IntegrationEvents/"]
COPY ["src/Modules/Ticketing/Crowbond.Modules.Ticketing.Presentation/Crowbond.Modules.Ticketing.Presentation.csproj", "src/Modules/Ticketing/Crowbond.Modules.Ticketing.Presentation/"]

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

RUN dotnet restore "./src/API/Crowbond.Api/Crowbond.Api.csproj"
COPY . .
WORKDIR "src/API/Crowbond.Api"
RUN dotnet build "./Crowbond.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Crowbond.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Crowbond.Api.dll"]