using System.Text.Json.Serialization;

namespace Crowbond.Modules.OMS.Domain.Webhooks.Choco;

public class ChocoOrderCreatedWebhook
{
    [JsonPropertyName("webhookEventId")] public string WebhookEventId { get; set; } = string.Empty;

    [JsonPropertyName("adapterId")] public string AdapterId { get; set; } = string.Empty;

    [JsonPropertyName("actionId")] public string ActionId { get; set; } = string.Empty;

    [JsonPropertyName("connectionId")] public string ConnectionId { get; set; } = string.Empty;

    [JsonPropertyName("vendorId")] public string VendorId { get; set; } = string.Empty;

    [JsonPropertyName("deliveryAttempt")] public int DeliveryAttempt { get; set; }

    [JsonPropertyName("deliveryAttemptedAt")]
    public DateTime DeliveryAttemptedAt { get; set; }

    [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; set; }

    [JsonPropertyName("payload")] public ChocoOrderCreatedPayload Payload { get; set; } = new();
}

public class ChocoOrderCreatedPayload
{
    [JsonPropertyName("eventType")] public string EventType { get; set; } = string.Empty;

    [JsonPropertyName("order")] public ChocoOrder Order { get; set; } = new();
}

public class ChocoOrder
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("referenceNumber")] public string ReferenceNumber { get; set; } = string.Empty;

    [JsonPropertyName("deliveryDate")] public DateTime DeliveryDate { get; set; }

    [JsonPropertyName("comment")] public string Comment { get; set; } = string.Empty;

    [JsonPropertyName("vendorReferenceNumber")]
    public string VendorReferenceNumber { get; set; } = string.Empty;

    [JsonPropertyName("confirmedAt")] public DateTime? ConfirmedAt { get; set; }

    [JsonPropertyName("createdAt")] public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")] public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("status")] public string Status { get; set; } = string.Empty;

    [JsonPropertyName("customer")] public ChocoCustomer Customer { get; set; } = new();

    [JsonPropertyName("contact")] public ChocoContact Contact { get; set; } = new();

    [JsonPropertyName("products")] public List<ChocoOrderProduct> Products { get; set; } = new();

    [JsonPropertyName("poNumber")] public string PoNumber { get; set; } = string.Empty;

    [JsonPropertyName("createdByContext")] public string CreatedByContext { get; set; } = string.Empty;
}

public class ChocoCustomer
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("customerNumber")] public string CustomerNumber { get; set; } = string.Empty;

    // Added property to match the payload
    [JsonPropertyName("companyId")] public string CompanyId { get; set; } = string.Empty;

    [JsonPropertyName("referenceNumber")] public string ReferenceNumber { get; set; } = string.Empty;

    [JsonPropertyName("deliveryAddress")] public ChocoAddress DeliveryAddress { get; set; } = new();

    [JsonPropertyName("companyAddress")] public ChocoAddress CompanyAddress { get; set; } = new();

    [JsonPropertyName("utcOffsetMinutes")] public int UtcOffsetMinutes { get; set; }
}

public class ChocoAddress
{
    [JsonPropertyName("full")] public string Full { get; set; } = string.Empty;

    [JsonPropertyName("country")] public string Country { get; set; } = string.Empty;

    [JsonPropertyName("city")] public string City { get; set; } = string.Empty;

    [JsonPropertyName("streetName")] public string StreetName { get; set; } = string.Empty;

    [JsonPropertyName("streetNumber")] public string StreetNumber { get; set; } = string.Empty;

    [JsonPropertyName("administrativeAreaLevelOne")]
    public string AdministrativeAreaLevelOne { get; set; } = string.Empty;

    [JsonPropertyName("postalCode")] public string PostalCode { get; set; } = string.Empty;
}

public class ChocoContact
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("phone")] public string Phone { get; set; } = string.Empty;

    [JsonPropertyName("email")] public string Email { get; set; } = string.Empty;
}

public class ChocoOrderProduct
{
    [JsonPropertyName("productId")] public string ProductId { get; set; } = string.Empty;

    // Using string to match the JSON (e.g., "8")
    [JsonPropertyName("quantity")] public string Quantity { get; set; } = string.Empty;

    [JsonPropertyName("product")] public ChocoProduct Product { get; set; } = new();

    [JsonPropertyName("unitPrice")] public ChocoPrice UnitPrice { get; set; } = new();

    [JsonPropertyName("totalAmount")] public ChocoPrice TotalAmount { get; set; } = new();

    [JsonPropertyName("comment")] public string Comment { get; set; } = string.Empty;
}

public class ChocoProduct
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;

    [JsonPropertyName("vendorId")] public string VendorId { get; set; } = string.Empty;

    [JsonPropertyName("externalId")] public string ExternalId { get; set; } = string.Empty;

    [JsonPropertyName("variantGroupCode")] public string VariantGroupCode { get; set; } = string.Empty;

    [JsonPropertyName("categoryName")] public string CategoryName { get; set; } = string.Empty;

    [JsonPropertyName("subCategoryName")] public string SubCategoryName { get; set; } = string.Empty;

    [JsonPropertyName("isActive")] public bool IsActive { get; set; }

    [JsonPropertyName("packSize")] public string PackSize { get; set; } = string.Empty;

    [JsonPropertyName("unit")] public string Unit { get; set; } = string.Empty;

    [JsonPropertyName("unitPrice")] public ChocoPrice UnitPrice { get; set; } = new();

    [JsonPropertyName("baseUnit")] public string BaseUnit { get; set; } = string.Empty;

    [JsonPropertyName("conversionFactor")] public ChocoConversionFactor ConversionFactor { get; set; } = new();

    [JsonPropertyName("leadTimeDays")] public int? LeadTimeDays { get; set; }

    [JsonPropertyName("cutOffTime")] public string CutOffTime { get; set; } = string.Empty;

    [JsonPropertyName("brand")] public string Brand { get; set; } = string.Empty;

    [JsonPropertyName("ean")] public string Ean { get; set; } = string.Empty;

    [JsonPropertyName("upc")] public string Upc { get; set; } = string.Empty;

    [JsonPropertyName("origin")] public string Origin { get; set; } = string.Empty;

    [JsonPropertyName("minimumOrderingQuantity")]
    public string MinimumOrderingQuantity { get; set; } = string.Empty;

    [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")] public DateTime UpdatedAt { get; set; }
}

public class ChocoPrice
{
    [JsonPropertyName("currency")] public string Currency { get; set; } = string.Empty;

    // Amount is represented as a string in the payload
    [JsonPropertyName("amount")] public string Amount { get; set; } = string.Empty;
}

public class ChocoConversionFactor
{
    // Map JSON key "decimal" to a safe C# property name.
    [JsonPropertyName("decimal")] public string DecimalValue { get; set; } = string.Empty;

    [JsonPropertyName("fractional")] public string Fractional { get; set; } = string.Empty;
}
