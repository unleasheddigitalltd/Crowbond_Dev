﻿namespace Crowbond.Modules.WMS.Domain.Products;

public interface IProductRepository
{
    Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<FilterType?> GetFilterTypeAsync(string name, CancellationToken cancellationToken = default);

    Task<InventoryType?> GetInventoryTypeAsync(string name, CancellationToken cancellationToken = default);

    Task<UnitOfMeasure?> GetUnitOfMeasureAsync(string name, CancellationToken cancellationToken = default);

    Task<Category?> GetCategoryAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Brand?> GetBrandAsync(Guid id, CancellationToken cancellationToken = default);

    Task<ProductGroup?> GetProductGroupAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Product product);

    void InsertCategory(Category category);

    void InsertBrand(Brand brand);

    void InsertProductGroup(ProductGroup productGroup);
}
