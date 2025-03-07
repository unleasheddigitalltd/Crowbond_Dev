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

variable "domain_name" {
  description = "Domain name for the application"
  type        = string
  default     = "api.unleashed-erp.com"
}

variable "route53_zone_id" {
  description = "Route53 hosted zone ID"
  type        = string
  default     = "Z09506483H2VGG00RCJWR"
}

variable "certificate_arn" {
  description = "ARN of the ACM certificate to use for HTTPS"
  type        = string
  default     = "arn:aws:acm:eu-west-1:499713595443:certificate/00db8056-351d-4516-b345-32266be21c77"
}