namespace Crowbond.Modules.CRM.Presentation;

internal static class Permissions
{
    internal const string GetCustomers = "customers:read";
    internal const string ModifyCustomers = "customers:update";
    internal const string CreateCustomers = "customers:create";

    internal const string ModifyCustomerContacts = "customers:contacts:update";
    internal const string CreateCustomerContacts = "customers:contacts:create";

    internal const string ModifyCustomerOutlets = "customers:outlets:update";
    internal const string CreateCustomerOutlets = "customers:outlets:create";

    internal const string GetSuppliers = "suppliers:read";
    internal const string ModifySuppliers = "suppliers:update";
    internal const string CreateSuppliers = "suppliers:create";

    internal const string ModifySupplierContacts = "suppliers:contacts:update";
    internal const string CreateSupplierContacts = "suppliers:contacts:create";

    internal const string ModifySupplierProducts = "suppliers:products:read";

    internal const string GetPriceTiers = "price-tiers:read";
    internal const string ModifyPriceTiers = "price-tiers:update";
}
