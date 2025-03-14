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


# Output the policy ARN for reference
output "terraform_state_policy_arn" {
  value       = aws_iam_policy.terraform_state.arn
  description = "ARN of the Terraform state management policy"
}