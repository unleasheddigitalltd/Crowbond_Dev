﻿using FluentValidation;

namespace Crowbond.Modules.Events.Application.TicketTypes.CreateTicketType;

internal sealed class CreateTicketTypeCommandValidator : AbstractValidator<CreateTicketTypeCommand>
{
    public CreateTicketTypeCommandValidator()
    {
        RuleFor(c => c.EventId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Price).GreaterThan(decimal.Zero);
        RuleFor(c => c.Currency).NotEmpty();
        RuleFor(c => c.Quantity).GreaterThan(decimal.Zero);
    }
}
