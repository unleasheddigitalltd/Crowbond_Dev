using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Products.CreateProduct;
using Crowbond.Modules.WMS.Application.Products.GetProduct;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Crowbond.Modules.WMS.IntegrationTests.Products;

public class GetProductTests : BaseIntegrationTest
{
    public GetProductTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
        
    }

    [Fact]
    public async Task Should_ReturnError_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = Guid.NewGuid();

        // Act
        Result<ProductResponse> productResult = await Sender.Send(new GetProductQuery(productId));

        // Assert
        productResult.Error.Should().Be(ProductErrors.NotFound(productId));
    }
}
