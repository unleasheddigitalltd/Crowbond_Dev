﻿using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Application.Customers.UpdateCustomerActivation;

internal sealed class UpdateCustomerActivationCommandHandler(
    ICustomerRepository customerRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCustomerActivationCommand>
{
    public async Task<Result> Handle(UpdateCustomerActivationCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetAsync(request.CustomerId, cancellationToken);

        if (customer == null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        Result result = request.IsActive ? customer.Activate(request.UserId, dateTimeProvider.UtcNow) : customer.Deactivate(request.UserId, dateTimeProvider.UtcNow);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}