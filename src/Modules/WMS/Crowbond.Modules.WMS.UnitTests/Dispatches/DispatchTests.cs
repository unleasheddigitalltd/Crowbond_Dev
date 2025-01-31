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

    [Fact]
    public void AddLine_ShouldReturnNewDispatchLine_WhenInputIsValid()
    {
        // Arrange
        string dispatchNo = Faker.Random.AlphaNumeric(10);
        var routeTripId = Guid.NewGuid();
        DateOnly routeTripDate = Faker.Date.RecentDateOnly();
        string routeName = Faker.Address.City();
        Result<DispatchHeader> header = DispatchHeader.Create(dispatchNo, routeTripId, routeTripDate, routeName);
        var orderId = Guid.NewGuid();
        string orderNo = Faker.Random.AlphaNumeric(10);
        string customerBusinessName = Faker.Company.Locale;
        var orderLineId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        decimal orderQty = Faker.Random.Decimal(0);
        bool isBulk = false;

        // Act
        Result<DispatchLine> result = header.Value.AddLine(orderId, orderNo, customerBusinessName, orderLineId, productId, orderQty, isBulk);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.OrderId.Should().Be(orderId);
        result.Value.OrderNo.Should().Be(orderNo);
        result.Value.CustomerBusinessName.Should().Be(customerBusinessName);
        result.Value.OrderLineId.Should().Be(orderLineId);
        result.Value.ProductId.Should().Be(productId);
        result.Value.OrderedQty.Should().Be(orderQty);
        result.Value.PickedQty.Should().Be(0);
        result.Value.IsPicked.Should().Be(false);
        header.Value.Lines.First().Should().Be(result.Value);
    }
}
