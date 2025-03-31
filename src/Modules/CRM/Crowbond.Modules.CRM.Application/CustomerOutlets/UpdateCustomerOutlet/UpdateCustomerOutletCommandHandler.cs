using System.Globalization;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Application.Routes;
using Crowbond.Modules.CRM.Domain.CustomerOutlets;
using Crowbond.Modules.CRM.Domain.Routes;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.UpdateCustomerOutlet;

internal sealed class UpdateCustomerOutletCommandHandler(
    RouteService routeService,
    IRouteRepository routeRepository,
    ICustomerOutletRepository customerOutletRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCustomerOutletCommand>
{
    public async Task<Result> Handle(UpdateCustomerOutletCommand request, CancellationToken cancellationToken)
    {

        string[] validFormats = ["HH:mm", "HH:mm:ss"];

        if (!TimeOnly.TryParseExact(request.CustomerOutlet.DeliveryTimeFrom, validFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out TimeOnly deliveryTimeFrom))
        {
            return Result.Failure(CustomerOutletErrors.InvalidTimeFormat(nameof(request.CustomerOutlet.DeliveryTimeFrom)));
        }

        if (!TimeOnly.TryParseExact(request.CustomerOutlet.DeliveryTimeTo, validFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out TimeOnly deliveryTimeTo))
        {
            return Result.Failure(CustomerOutletErrors.InvalidTimeFormat(nameof(request.CustomerOutlet.DeliveryTimeTo)));
        }

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
            deliveryTimeFrom,
            deliveryTimeTo,
            request.CustomerOutlet.Is24HrsDelivery);

            customerOutlet.RemoveRoutes();
            customerOutletRepository.RemoveRoutes(customerOutlet.Routes);

        foreach (CustomerOutletRouteRequest routeRequest in request.CustomerOutlet.Routes)
        {
            Route route = await routeRepository.GetAsync(routeRequest.RouteId, cancellationToken);

            if (route is null)
            {
                return Result.Failure<Guid>(RouteErrors.NotFound(routeRequest.RouteId));
            }

            if (!routeService.IsDayActive(route.DaysOfWeek, routeRequest.Weekday))
            {
                return Result.Failure<Guid>(RouteErrors.DayIsNotAvailable(route.Id, routeRequest.Weekday));
            }

            Result<CustomerOutletRoute> routeResult = customerOutlet.AddRoute(routeRequest.RouteId, routeRequest.Weekday);

            if (routeResult.IsFailure)
            {
                return Result.Failure<Guid>(routeResult.Error);
            }

            customerOutletRepository.InsertRoute(routeResult.Value);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
