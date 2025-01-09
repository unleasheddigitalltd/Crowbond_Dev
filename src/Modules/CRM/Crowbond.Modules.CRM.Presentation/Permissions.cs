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

    internal const string CreateCustomerProducts = "customers:products:create";
    internal const string ModifyCustomerProducts = "customers:products:update";
    internal const string GetCustomerProducts = "customers:products:read";
    internal const string DeleteCustomerProducts = "customers:products:delete";

    internal const string CreateCustomerProductBlacklist = "customers:product-blacklist:create";
    internal const string ModifyCustomerProductBlacklist = "customers:product-blacklist:update";
    internal const string GetCustomerProductBlacklist = "customers:product-blacklist:read";
    internal const string DeleteCustomerProductBlacklist = "customers:product-blacklist:delete";

    internal const string GetSuppliers = "suppliers:read";
    internal const string ModifySuppliers = "suppliers:update";
    internal const string CreateSuppliers = "suppliers:create";

    internal const string ModifySupplierContacts = "suppliers:contacts:update";
    internal const string CreateSupplierContacts = "suppliers:contacts:create";

    internal const string ModifySupplierProducts = "suppliers:products:update";

    internal const string GetPriceTiers = "price-tiers:read";
    internal const string ModifyPriceTiers = "price-tiers:update";

    internal const string GetSettings = "settings:read";
    internal const string ModifySettings = "settings:update";
}
