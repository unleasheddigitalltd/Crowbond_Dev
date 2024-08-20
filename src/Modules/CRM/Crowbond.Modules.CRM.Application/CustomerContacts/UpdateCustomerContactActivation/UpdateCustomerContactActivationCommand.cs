﻿using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.UpdateCustomerContactActivation;

public sealed record UpdateCustomerContactActivationCommand(Guid UserId, Guid CustomerContactId, bool IsActive) : ICommand;