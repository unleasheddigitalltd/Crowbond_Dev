using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.UnitTests.Abstractions;
using FluentAssertions;

namespace Crowbond.Modules.WMS.UnitTests.Locations;

public class LocationTests : BaseTest
{
    [Fact]
    public void Create_ShouldReturnNewLocation_WhenInputIsValid()
    {
        // Arrange
        var parentId = Guid.NewGuid();
        string name = Faker.Random.AlphaNumeric(10);
        string scanCode = Faker.Random.AlphaNumeric(10);
        LocationType locationType = LocationType.Putaway;
        LocationLayer locationLayer = LocationLayer.Location;

        // Act
        Result<Location> result = Location.Create(parentId, name, scanCode, locationType, locationLayer);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ParentId.Should().Be(parentId);
        result.Value.Name.Should().Be(name);
        result.Value.ScanCode.Should().Be(scanCode);
        result.Value.LocationType.Should().Be(locationType);
        result.Value.LocationLayer.Should().Be(locationLayer);
        result.Value.Status.Should().Be(LocationStatus.Active);
    }
}
