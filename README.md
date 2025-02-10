# Crowbond_Dev

A .NET-based application with supporting services.

## Prerequisites

- [Docker](https://www.docker.com/products/docker-desktop)
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker Compose](https://docs.docker.com/compose/install/)

## Project Structure

The project consists of several services:

- **Crowbond.Api**: Main API service (.NET)
- **Crowbond.Database**: PostgreSQL database
- **Crowbond.Identity**: Keycloak identity service
- **Crowbond.Seq**: Seq logging service

## Getting Started

1. Clone the repository
2. Navigate to the project root directory

### Running with Docker Compose

```bash
# First time setup or when code changes are made
docker compose up --build -d

# For subsequent runs when no code changes
docker compose up -d
```

This will start all services:

- API: http://localhost:5002
- Keycloak: http://localhost:18080
- PostgreSQL: localhost:5432
- Seq: http://localhost:80

### Development Credentials

#### Keycloak Admin Console

- URL: http://localhost:18080/admin
- Username: admin
- Password: admin

#### Database

- Database: crowbond
- Username: postgres
- Password: postgres

## Development

To run the project for development:

1. Ensure all containers are running using `docker compose up -d`
2. Open the solution in your preferred IDE
3. Set the API project as the startup project
4. Run the project

## Configuration

Environment-specific settings can be found in:

- `appsettings.Development.json` for development settings
- Docker environment variables in `docker-compose.yml`

## Infrastructure

The project includes Terraform configurations for infrastructure management. See the `/terraform` directory for details.
