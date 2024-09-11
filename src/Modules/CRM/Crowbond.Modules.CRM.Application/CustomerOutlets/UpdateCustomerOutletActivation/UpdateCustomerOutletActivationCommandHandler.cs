using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerOutlets;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.UpdateCustomerOutletActivation;

internal sealed class UpdateCustomerOutletActivationCommandHandler(
    ICustomerOutletRepository customerOutletRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCustomerOutletActivationCommand>
{
    public async Task<Result> Handle(UpdateCustomerOutletActivationCommand request, CancellationToken cancellationToken)
    {
        CustomerOutlet? outlet = await customerOutletRepository.GetAsync(request.CustomerOutletId, cancellationToken);

        if (outlet == null)
        {
            return Result.Failure(CustomerOutletErrors.NotFound(request.CustomerOutletId));
        }

        Result result = request.IsActive ? outlet.Activate() : outlet.Deactivate();

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
