﻿using Crowbond.Common.Application.Messaging;
using Microsoft.AspNetCore.Http;

namespace Crowbond.Modules.CRM.Application.Customers.UploadCustomerLogo;

public sealed record UploadCustomerLogoCommand(Guid UserId, Guid CustomerId, IFormFile Logo) : ICommand;