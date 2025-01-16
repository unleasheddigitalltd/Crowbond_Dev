using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.UnitTests.Abstractions;
using FluentAssertions;

namespace Crowbond.Modules.WMS.UnitTests.Dispatches;

public class DispatchTests : BaseTest
{
    [Fact]
    public void Create_ShouldReturnNewDispatchHeader_WhenInputIsValid()
    {
        // Arrange
        string dispatchNo = Faker.Random.AlphaNumeric(10);
        var routeTripId = Guid.NewGuid();
        DateOnly routeTripDate = Faker.Date.RecentDateOnly();
        string routeName = Faker.Address.City();

        // Act
        Result<DispatchHeader> result = DispatchHeader.Create(dispatchNo, routeTripId, routeTripDate, routeName);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.DispatchNo.Should().Be(dispatchNo);
        result.Value.RouteTripId.Should().Be(routeTripId);
        result.Value.RouteTripDate.Should().Be(routeTripDate);
        result.Value.RouteName.Should().Be(routeName);
        result.Value.Status.Should().Be(DispatchStatus.NotStarted);
        result.Value.Lines.Should().BeEmpty();
    }
}
