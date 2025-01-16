using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Stocks;
using Crowbond.Modules.WMS.Application.Stocks.GetStock;
using Crowbond.Modules.WMS.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Crowbond.Modules.WMS.IntegrationTests.Stocks;

public class GetStockTests : BaseIntegrationTest
{
    public GetStockTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
        
    }

    [Fact]
    public async Task Should_ReturnError_WhenStockDoesNotExist()
    {
        // Arrange
        var stockId = Guid.NewGuid();

        // Act
        Result<StockResponse> productResult = await Sender.Send(new GetStockQuery(stockId));

        // Assert
        productResult.Error.Should().Be(StockErrors.NotFound(stockId));
    }
}
