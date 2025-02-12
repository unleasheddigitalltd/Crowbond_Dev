resource "aws_ecr_repository" "api" {
  name         = "${var.app_name}-${var.environment}"
  force_delete = true
}

# Create App Runner access role
resource "aws_iam_role" "app_runner" {
  name = "${var.app_name}-${var.environment}-role"

  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Action = "sts:AssumeRole"
        Effect = "Allow"
        Principal = {
          Service = [
            "build.apprunner.amazonaws.com",
            "tasks.apprunner.amazonaws.com"
          ]
        }
      }
    ]
  })
}

# Attach ECR access policy to the role
resource "aws_iam_role_policy_attachment" "app_runner_ecr_policy" {
  role       = aws_iam_role.app_runner.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AWSAppRunnerServicePolicyForECRAccess"
}

resource "aws_apprunner_service" "api" {
  service_name = "${var.app_name}-${var.environment}"

  source_configuration {
    authentication_configuration {
      access_role_arn = aws_iam_role.app_runner.arn
    }

    image_repository {
      image_configuration {
        port = "8080"
        runtime_environment_variables = {
          ASPNETCORE_ENVIRONMENT = "Production"
          DB_PASSWORD           = var.db_password
        }
      }
      image_identifier      = "${aws_ecr_repository.api.repository_url}:latest"
      image_repository_type = "ECR"
    }
  }

  instance_configuration {
    cpu    = "1 vCPU"
    memory = "2 GB"
  }

  tags = {
    Environment = var.environment
    Project     = "crowbond"
  }
}

output "service_url" {
  value = aws_apprunner_service.api.service_url
}

output "ecr_repository_url" {
  value       = aws_ecr_repository.api.repository_url
  description = "URL of the ECR repository"
}