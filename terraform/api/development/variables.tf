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