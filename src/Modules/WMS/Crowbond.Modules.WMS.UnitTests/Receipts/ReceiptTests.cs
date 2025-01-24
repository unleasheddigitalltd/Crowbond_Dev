using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.UnitTests.Abstractions;
using FluentAssertions;

namespace Crowbond.Modules.WMS.UnitTests.Receipts;

public class ReceiptTests : BaseTest
{
    [Fact]
    public void Create_ShouldReturnNewReceiptHeader_WhenInputIsValid()
    {
        // Arrange
        string receiptNo = Faker.Random.AlphaNumeric(10);
        var puchaseOrderId = Guid.NewGuid();
        string puchaseOrderNo = Faker.Random.AlphaNumeric(10);

        // Act
        Result<ReceiptHeader> result = ReceiptHeader.Create(receiptNo, puchaseOrderId, puchaseOrderNo);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ReceiptNo.Should().Be(receiptNo);
        result.Value.PurchaseOrderId.Should().Be(puchaseOrderId);
        result.Value.PurchaseOrderNo.Should().Be(puchaseOrderNo);
        result.Value.Status.Should().Be(ReceiptStatus.Shipping);
    }
}
