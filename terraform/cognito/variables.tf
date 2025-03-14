variable "environment" {
  description = "Environment name (e.g., dev, staging, prod)"
  type        = string
}

variable "application_name" {
  description = "Name of the application"
  type        = string
  default     = "crowbond"
}