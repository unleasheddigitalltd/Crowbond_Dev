#pragma warning disable S125
// using Crowbond.Common.Application.Clock;
#pragma warning restore S125
// using Crowbond.Common.Infrastructure.ScheduledJob;
// using Crowbond.Modules.OMS.Domain.RouteTrips;
//
// namespace Crowbond.Modules.OMS.Infrastructure.RouteTrips;
//
// public class RemoveOrdersFromExpiredRouteTripsJob(IRouteTripRepository repository, IDateTimeProvider dateTimeProvider) : IScheduledJob
// {
//     private readonly IRouteTripRepository _repository = repository;
//     private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
//
//     public async Task ExecuteAsync(CancellationToken cancellationToken = default)
//     {
//         // var today = _dateTimeProvider.UtcNow; 
//         // var yesterday = _dateTimeProvider.UtcNow.AddDays(-1);
//         // var routesForYesterday =  await _repository.GetByDateAsync(DateOnly.FromDateTime(yesterday), cancellationToken);
//         // var expiredRouteTrips = routesForYesterday?.Where(r => r.IsExpired(DateOnly.FromDateTime(today)));
//         //remove all orders from these routes
//         
//     }
// }
