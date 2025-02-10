resource "aws_db_instance" "crowbond" {
  identifier        = "crowbond-${var.environment}"
  engine            = "postgres"
  engine_version    = "17"
  instance_class    = "db.t3.micro"
  allocated_storage = 20

  db_name  = var.db_name
  username = var.db_username
  password = var.db_password

  skip_final_snapshot    = true
  publicly_accessible    = true  # Set to false in production
  vpc_security_group_ids = [aws_security_group.rds.id]

  backup_retention_period = 7
  backup_window          = "03:00-04:00"
  maintenance_window     = "Mon:04:00-Mon:05:00"

  tags = {
    Environment = var.environment
    Project     = "crowbond"
  }
}

resource "aws_security_group" "rds" {
  name_prefix = "crowbond-rds-${var.environment}"
  description = "Security group for Crowbond RDS instance"

  ingress {
    from_port   = 5432
    to_port     = 5432
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]  # Update this to production IP range
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }

  tags = {
    Environment = var.environment
    Project     = "crowbond"
  }
}

output "rds_endpoint" {
  value = aws_db_instance.crowbond.endpoint
}