variable "environment" {
  type    = string
  default = "production"
}

variable "app_name" {
  type    = string
  default = "crowbond-api"
}

variable "db_host" {
  type    = string
  default = "crowbond-production.cniqiksigl64.eu-west-1.rds.amazonaws.com"
}

variable "db_port" {
  type    = string
  default = "5432"
}

variable "db_name" {
  type    = string
  default = "crowbond"
}

variable "db_username" {
  type = string
  default = "postgres"
}

variable "db_password" {
  type      = string
  sensitive = true
}

variable "cognito_client_secret" {
  description = "AWS Cognito User Pool Client Secret"
  type        = string
  sensitive   = true
  default     = "1qpfkaphq814i2kp07a6mc0v04l5iglb3r9k4g0c7c784jriu3uh"
}

variable "cognito_user_pool_arn" {
  description = "ARN of the Cognito user pool"
  type        = string
  default     = "arn:aws:cognito-idp:eu-west-1:499713595443:userpool/eu-west-1_RYVPEbdrb"
}