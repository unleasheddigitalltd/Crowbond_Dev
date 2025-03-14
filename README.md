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
3. Set up required environment variables:
   ```bash
   export DB_PASSWORD="your_database_password"  # Required for both local development and deployment
   ```

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
2. Set required environment variables:
   ```bash
   export DB_PASSWORD="your_database_password"  # Must match the password in your local PostgreSQL container
   ```
3. Open the solution in your preferred IDE
4. Set the API project as the startup project
5. Run the project

> Note: The `DB_PASSWORD` environment variable is required for:
>
> - Local development with the API
> - Running Docker Compose
> - Terraform infrastructure changes
> - GitHub Actions deployments

## Configuration

Environment-specific settings can be found in:

- `appsettings.Development.json` for development settings
- Docker environment variables in `docker-compose.yml`

## Infrastructure

The project includes Terraform configurations for infrastructure management in the `/terraform` directory:

- `/terraform/api`: API infrastructure (ECS, ECR, VPC, etc.)
- `/terraform/rds`: Database infrastructure

### Required Secrets

The following secrets need to be configured:

#### GitHub Actions Secrets

- `AWS_ACCESS_KEY_ID`: AWS access key for deployment
- `AWS_SECRET_ACCESS_KEY`: AWS secret key for deployment
- `DB_PASSWORD`: Database password for RDS instance

#### Local Development Environment Variables

When running Terraform locally, you'll need:

```bash
export TF_VAR_db_password="your_database_password"
```

### Deployment

The project uses GitHub Actions for CI/CD with two main workflows:

1. **Terraform Workflow** (`terraform.yml`)

   - Triggered by changes to `/terraform/**`
   - Manages infrastructure changes
   - Requires `DB_PASSWORD` secret for RDS configuration

2. **API Deployment** (`deploy.yml`)
   - Triggered by changes to `src/API/**`
   - Builds and deploys the API to ECS
   - Builds for linux/amd64 platform
   - Tags images with both commit SHA and 'latest'

The API is deployed to AWS ECS (Fargate) and uses:

- ECR for container registry
- RDS for PostgreSQL database
- VPC with public subnets

### Manual Deployment

If you need to deploy manually:

```bash
# Build the Docker image
docker build --platform linux/amd64 -t crowbond-api .

# Push to ECR (after aws ecr login)
docker tag crowbond-api:latest $ECR_REGISTRY/crowbond-api:latest
docker push $ECR_REGISTRY/crowbond-api:latest
```

### Infrastructure Updates

To apply infrastructure changes:

```bash
# For API infrastructure
cd terraform/api
terraform init
terraform plan -var-file="production.tfvars" -var="db_password=$DB_PASSWORD"
terraform apply -var-file="production.tfvars" -var="db_password=$DB_PASSWORD"

# For RDS infrastructure
cd terraform/rds
terraform init
terraform plan -var-file="production.tfvars" -var="db_password=$DB_PASSWORD"
terraform apply -var-file="production.tfvars" -var="db_password=$DB_PASSWORD"
```
