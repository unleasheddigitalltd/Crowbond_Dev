terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }

  backend "s3" {
    bucket  = "crowbond-terraform-state"
    key     = "api/terraform.tfstate"
    region  = "us-east-1"
  }
}

provider "aws" {
  region  = "eu-west-1"
  # Profile is handled by GitHub Actions credentials
}