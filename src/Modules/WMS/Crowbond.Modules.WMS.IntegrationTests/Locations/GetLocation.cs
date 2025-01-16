using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Locations.GetLocation;
using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Crowbond.Modules.WMS.IntegrationTests.Locations;

public class GetLocation : BaseIntegrationTest
{
    public GetLocation(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
        
    }

    [Fact]
    public async Task Should_ReturnError_WhenLocationDoesNotExist()
    {
        // Arrange
        var locationId = Guid.NewGuid();

        // Act
        Result<LocationResponse> productResult = await Sender.Send(new GetLocationQuery(locationId));

        // Assert
        productResult.Error.Should().Be(LocationErrors.NotFound(locationId));
    }
}
