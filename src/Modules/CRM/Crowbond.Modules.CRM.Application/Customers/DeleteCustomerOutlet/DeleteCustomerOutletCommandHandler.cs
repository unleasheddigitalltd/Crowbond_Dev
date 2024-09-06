using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerOutlets;

namespace Crowbond.Modules.CRM.Application.Customers.DeleteCustomerOutlet;

internal sealed class DeleteCustomerOutletCommandHandler(
    ICustomerOutletRepository customerOutletRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteCustomerOutletCommand>
{
    public async Task<Result> Handle(DeleteCustomerOutletCommand request, CancellationToken cancellationToken)
    {
        
        CustomerOutlet? outlet = await customerOutletRepository.GetAsync(request.CustomerOutletId, cancellationToken);

        if (outlet == null)
        {
            return Result.Failure(CustomerOutletErrors.NotFound(request.CustomerOutletId));
        }

        customerOutletRepository.Remove(outlet);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
