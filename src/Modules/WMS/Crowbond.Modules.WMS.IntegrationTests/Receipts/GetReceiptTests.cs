using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Receipts.GetReceiptHeader;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Crowbond.Modules.WMS.IntegrationTests.Receipts;

public class GetReceiptTests : BaseIntegrationTest
{
    public GetReceiptTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
        
    }

    [Fact]
    public async Task Should_ReturnError_WhenReceiptHeaderDoesNotExist()
    {
        // Arrange
        var receiptHeaderId = Guid.NewGuid();

        // Act
        Result<ReceiptResponse> productResult = await Sender.Send(new GetReceiptHeaderQuery(receiptHeaderId));

        // Assert
        productResult.Error.Should().Be(ReceiptErrors.NotFound(receiptHeaderId));
    }
}
