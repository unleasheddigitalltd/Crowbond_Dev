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

    [Fact]
    public void Create_ShouldReturnFailer_WhenLayerIsNotLocationAndTypeIsNotNull()
    {
        // Arrange
        var parentId = Guid.NewGuid();
        string name = Faker.Random.AlphaNumeric(10);
        string scanCode = Faker.Random.AlphaNumeric(10);
        LocationType? locationType = Enum.GetValues<LocationType>().FirstOrDefault();
        LocationLayer nonLocationLayer = Enum.GetValues(typeof(LocationLayer))
                               .Cast<LocationLayer>()
                               .FirstOrDefault(layer => layer != LocationLayer.Location);

        // Act
        Result<Location> result = Location.Create(parentId, name, scanCode, locationType, nonLocationLayer);

        // Assert
        result.Error.Should().Be(LocationErrors.InvalidLocationTypeAssignment);
    }

    [Fact]
    public void Create_ShouldReturnFailer_WhenParentIsNullAndLayerIsAreaOrLocation()
    {
        // Arrange
        Guid? parentId = null;
        string name = Faker.Random.AlphaNumeric(10);
        string scanCode = Faker.Random.AlphaNumeric(10);
        
        LocationLayer[] locationLayers = { LocationLayer.Area, LocationLayer.Location };

        foreach (LocationLayer layer in locationLayers)
        {
            LocationType? locationType = layer == LocationLayer.Location ? LocationType.Putaway : null;
            // Act
            Result<Location> result = Location.Create(parentId, name, scanCode, locationType, layer);

            // Assert
            result.Error.Should().Be(LocationErrors.NeedParent);
        }
    }

    [Fact]
    public void Create_ShouldReturnFailer_WhenLayerIsSiteAndParentIsNotNull()
    {
        // Arrange
        var parentId = Guid.NewGuid();
        string name = Faker.Random.AlphaNumeric(10);
        string scanCode = Faker.Random.AlphaNumeric(10);
        LocationType? locationType = null;
        LocationLayer locationLayer = LocationLayer.Site;

        // Act
        Result<Location> result = Location.Create(parentId, name, scanCode, locationType, locationLayer);

        // Assert
        result.Error.Should().Be(LocationErrors.CanNotHaveParent);
    }

    [Fact]
    public void Update_ShouldReturnSucces_WhenInputIsValid()
    {
        // Arrange
        var parentId = Guid.NewGuid();
        string name = Faker.Random.AlphaNumeric(10);
        string scanCode = Faker.Random.AlphaNumeric(10);
        LocationType locationType = LocationType.Putaway;
        LocationLayer locationLayer = LocationLayer.Location;

        Result<Location> location = Location.Create(parentId, name, scanCode, locationType, locationLayer);

        var newParentId = Guid.NewGuid();
        string newName = Faker.Random.AlphaNumeric(10);
        string newScanCode = Faker.Random.AlphaNumeric(10);
        LocationType? newLocationType = null;
        LocationLayer newLocationLayer = LocationLayer.Area;

        // Act
        Result result = location.Value.Update(newParentId, newName, newScanCode, newLocationType, newLocationLayer);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}
