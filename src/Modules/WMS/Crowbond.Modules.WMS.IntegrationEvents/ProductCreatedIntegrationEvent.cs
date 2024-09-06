﻿using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.WMS.IntegrationEvents;

public sealed class ProductCreatedIntegrationEvent : IntegrationEvent
{
    public ProductCreatedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid productId,
        string name,
        string sku,
        string filterTypeName,
        string unitOfMeasureName,
        string inventoryTypeName,
        Guid categoryId,
        string categoryName,
        int taxRateType,
        bool isActive)
        : base(id, occurredOnUtc)
    {
        ProductId = productId;
        Name = name;
        Sku = sku;
        FilterTypeName = filterTypeName;
        UnitOfMeasureName = unitOfMeasureName;
        InventoryTypeName = inventoryTypeName;
        CategoryId = categoryId;
        CategoryName = categoryName;
        TaxRateType = taxRateType;
        IsActive = isActive;
    }

    public Guid ProductId { get; private set; }

    public string Name { get; private set; }

    public string Sku { get; private set; }

    public string FilterTypeName { get; private set; }

    public string UnitOfMeasureName { get; private set; }

    public string InventoryTypeName { get; private set; }

    public Guid CategoryId { get; private set; }

    public string CategoryName { get; private set; }

    public int TaxRateType { get; private set; }

    public bool IsActive { get; private set; }
}