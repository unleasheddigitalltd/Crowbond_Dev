using System.Globalization;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerOutlets;
using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.CreateCustomerOutlet;

internal sealed class CreateCustomerOutletCommandHandler(
    ICustomerRepository customerRepository,
    ICustomerOutletRepository customerOutletRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCustomerOutletCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCustomerOutletCommand request, CancellationToken cancellationToken)
    {
        string[] validFormats = ["HH:mm", "HH:mm:ss"];

        if (!TimeOnly.TryParseExact(request.CustomerOutlet.DeliveryTimeFrom, validFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out TimeOnly deliveryTimeFrom))
        {
            return Result.Failure<Guid>(CustomerOutletErrors.InvalitTimeFormat(nameof(request.CustomerOutlet.DeliveryTimeFrom)));
        }

        if (!TimeOnly.TryParseExact(request.CustomerOutlet.DeliveryTimeTo, validFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out TimeOnly deliveryTimeTo))
        {
            return Result.Failure<Guid>(CustomerOutletErrors.InvalitTimeFormat(nameof(request.CustomerOutlet.DeliveryTimeTo)));
        }

        Customer? customer = await customerRepository.GetAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<Guid>(CustomerErrors.NotFound(request.CustomerId));
        }

        Result<CustomerOutlet> result = CustomerOutlet.Create(
            request.CustomerId,
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
            deliveryTimeFrom,
            deliveryTimeTo,
            request.CustomerOutlet.Is24HrsDelivery);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        customerOutletRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
