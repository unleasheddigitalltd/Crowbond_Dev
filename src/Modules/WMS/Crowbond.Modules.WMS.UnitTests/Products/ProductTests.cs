using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.UnitTests.Abstractions;
using FluentAssertions;

namespace Crowbond.Modules.WMS.UnitTests.Products;

public class ProductTests : BaseTest
{
    [Fact]
    public void Create_ShouldRaiseDomainEvent_WhenProductCreated()
    {
        // Arrange

        // Act
        Result<Product> result = Product.Create(
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            null,
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            Guid.Empty,
            Guid.Empty,
            Guid.Empty,
            TaxRateType.NoVat,
            null,
            null,
            null,
            false,
            null,
            null,
            null,
            null,
            null,
            false);

        Product product = result.Value;

        // Assert
        ProductCreatedDomainEvent domainEvent = AssertDomainEventWasPublished<ProductCreatedDomainEvent>(product);

        domainEvent.ProductId.Should().Be(product.Id);
    }
}
