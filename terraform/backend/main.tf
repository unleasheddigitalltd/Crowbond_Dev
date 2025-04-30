resource "aws_s3_bucket" "terraform_state" {
  bucket = "crowbond-terraform-state"

  lifecycle {
    prevent_destroy = true
  }
}

resource "aws_s3_bucket_versioning" "terraform_state" {
  bucket = aws_s3_bucket.terraform_state.id
  versioning_configuration {
    status = "Enabled"
  }
}

resource "aws_s3_bucket_server_side_encryption_configuration" "terraform_state" {
  bucket = aws_s3_bucket.terraform_state.id

  rule {
    apply_server_side_encryption_by_default {
      sse_algorithm = "AES256"
    }
  }
}

# Create IAM policy for Terraform state management
resource "aws_iam_policy" "terraform_state" {
  name        = "terraform-state-management"
  description = "Policy for managing Terraform state in S3"

  policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Effect = "Allow"
        Action = [
          "s3:ListBucket",
          "s3:GetObject",
          "s3:PutObject",
          "s3:DeleteObject"
        ]
        Resource = [
          aws_s3_bucket.terraform_state.arn,
          "${aws_s3_bucket.terraform_state.arn}/*"
        ]
      }
    ]
  })
}

# Attach policy to your user
resource "aws_iam_user_policy_attachment" "terraform_state" {
  user       = "devteam" 
  policy_arn = aws_iam_policy.terraform_state.arn
}

# S3 bucket for images
resource "aws_s3_bucket" "images" {
  bucket = "crowbond-images"

  tags = {
    Name        = "Crowbond Images"
    Environment = "All"
    Purpose     = "Image Storage"
  }
}

# Configure CORS for the images bucket
resource "aws_s3_bucket_cors_configuration" "images" {
  bucket = aws_s3_bucket.images.id

  cors_rule {
    allowed_headers = ["*"]
    allowed_methods = ["GET", "PUT", "POST", "DELETE"]
    allowed_origins = ["*"]
    expose_headers  = ["ETag"]
    max_age_seconds = 3000
  }
}

# Configure public access settings for the images bucket
resource "aws_s3_bucket_public_access_block" "images" {
  bucket = aws_s3_bucket.images.id

  block_public_acls       = false
  block_public_policy     = false
  ignore_public_acls      = false
  restrict_public_buckets = false
}

# Add bucket policy to allow public read access to objects
resource "aws_s3_bucket_policy" "images_public_read" {
  bucket = aws_s3_bucket.images.id
  policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Sid       = "PublicReadGetObject"
        Effect    = "Allow"
        Principal = "*"
        Action    = "s3:GetObject"
        Resource  = "${aws_s3_bucket.images.arn}/*"
      }
    ]
  })

  # Ensure public access block is configured before applying policy
  depends_on = [aws_s3_bucket_public_access_block.images]
}

# Create IAM policy for images bucket access
resource "aws_iam_policy" "images_access" {
  name        = "images-bucket-access"
  description = "Policy for accessing the images S3 bucket"

  policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Effect = "Allow"
        Action = [
          "s3:ListBucket"
        ]
        Resource = [
          aws_s3_bucket.images.arn
        ]
      },
      {
        Effect = "Allow"
        Action = [
          "s3:GetObject",
          "s3:PutObject",
          "s3:DeleteObject"
        ]
        Resource = [
          "${aws_s3_bucket.images.arn}/*"
        ]
      }
    ]
  })
}

# Output the policy ARN for reference
output "terraform_state_policy_arn" {
  value       = aws_iam_policy.terraform_state.arn
  description = "ARN of the Terraform state management policy"
}

# Output the images bucket name and ARN
output "images_bucket_name" {
  value       = aws_s3_bucket.images.bucket
  description = "Name of the S3 bucket for images"
}

output "images_bucket_arn" {
  value       = aws_s3_bucket.images.arn
  description = "ARN of the S3 bucket for images"
}

output "images_bucket_policy_arn" {
  value       = aws_iam_policy.images_access.arn
  description = "ARN of the images bucket access policy"
}