terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }

  backend "s3" {}  # Empty backend config, will be provided by -backend-config
}

provider "aws" {
  region  = "eu-west-1"
  # profile = "crowbond-dev"  # Add profile for local development, GitHub Actions will override
}