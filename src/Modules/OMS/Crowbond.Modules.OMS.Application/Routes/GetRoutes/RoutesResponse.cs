using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.OMS.Application.Routes.GetRoutes;

public sealed class RoutesResponse : PaginatedResponse<Route>
{
    public RoutesResponse(IReadOnlyCollection<Route> locations, IPagination pagination)
        : base(locations, pagination)
    { }
}

public sealed record Route(Guid Id, string Name, int Position, TimeOnly CutOffTime, string DaysOfWeek);
