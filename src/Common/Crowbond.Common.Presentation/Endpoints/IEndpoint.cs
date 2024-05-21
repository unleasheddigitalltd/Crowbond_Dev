using Microsoft.AspNetCore.Routing;

namespace Crowbond.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
