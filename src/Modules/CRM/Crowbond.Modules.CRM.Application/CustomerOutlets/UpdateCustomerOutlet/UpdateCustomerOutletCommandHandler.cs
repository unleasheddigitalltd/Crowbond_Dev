using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerOutlets;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.UpdateCustomerOutlet;

internal sealed class UpdateCustomerOutletCommandHandler(
    ICustomerOutletRepository customerOutletRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCustomerOutletCommand>
{
    public async Task<Result> Handle(UpdateCustomerOutletCommand request, CancellationToken cancellationToken)
    {
        CustomerOutlet? customerOutlet = await customerOutletRepository.GetAsync(request.CustomerOutletId, cancellationToken);

        if (customerOutlet is null)
        {
            return Result.Failure(CustomerOutletErrors.NotFound(request.CustomerOutletId));        
        }

        customerOutlet.Update(
            request.CustomerOutlet.LocationName,
            request.CustomerOutlet.FullName,
            request.CustomerOutlet.Email,
            request.CustomerOutlet.PhoneNumber,
            request.CustomerOutlet.Mobile,
            request.CustomerOutlet.AddressLine1,
            request.CustomerOutlet.AddressLine2,
            request.CustomerOutlet.TownCity,
            request.CustomerOutlet.County,
            request.CustomerOutlet.Country,
            request.CustomerOutlet.PostalCode,
            request.CustomerOutlet.DeliveryNote,
            request.CustomerOutlet.DeliveryTimeFrom,
            request.CustomerOutlet.DeliveryTimeTo,
            request.CustomerOutlet.Is24HrsDelivery,
            request.UserId,
            dateTimeProvider.UtcNow);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
