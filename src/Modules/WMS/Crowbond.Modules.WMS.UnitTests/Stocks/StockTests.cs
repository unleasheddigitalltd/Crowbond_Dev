using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Stocks;
using Crowbond.Modules.WMS.UnitTests.Abstractions;
using FluentAssertions;
using FluentAssertions.Equivalency;

namespace Crowbond.Modules.WMS.UnitTests.Stocks;

public class StockTests : BaseTest
{
    [Fact]
    public void Create_ShouldReturnNewStock_WhenInputIsValid()
    {
        // Arrange

        var productId = Guid.NewGuid();
        var locationId = Guid.NewGuid();
        string batchNumber = Faker.Random.AlphaNumeric(10); 
        DateOnly receivedDate = Faker.Date.RecentDateOnly();
        DateOnly sellByDate = Faker.Date.RecentDateOnly();
        DateOnly useByDate = Faker.Date.RecentDateOnly();
        var receiptLineId = Guid.NewGuid();
        string note = Faker.Company.Locale;

        // Act
        Result<Stock> result = Stock.Create(productId, locationId, batchNumber, receivedDate, sellByDate, useByDate, receiptLineId, note);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ProductId.Should().Be(productId);
        result.Value.LocationId.Should().Be(locationId);
        result.Value.BatchNumber.Should().Be(batchNumber);
        result.Value.ReceivedDate.Should().Be(receivedDate);
        result.Value.SellByDate.Should().Be(sellByDate);
        result.Value.UseByDate.Should().Be(useByDate);
        result.Value.ReceiptLineId.Should().Be(receiptLineId);
        result.Value.Note.Should().Be(note);
        result.Value.Status.Should().Be(StockStatus.Active);
    }
}
