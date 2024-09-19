using Crowbond.Modules.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.Users.Infrastructure.Users;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(p => p.Code);

        builder.Property(p => p.Code).HasMaxLength(100);

        builder.HasData(
            Permission.GetUser,
            Permission.ModifyUser,

            Permission.GetEvents,
            Permission.SearchEvents,
            Permission.ModifyEvents,
            Permission.GetTicketTypes,
            Permission.ModifyTicketTypes,
            Permission.GetCategories,
            Permission.ModifyCategories,
            Permission.GetOrders,
            Permission.CreateOrder,
            Permission.GetTickets,
            Permission.CheckInTicket,
            Permission.GetEventStatistics,

            Permission.GetProducts,
            Permission.CreateProducts,
            Permission.ModifyProducts,

            Permission.GetStocks,
            Permission.GetStockTransactions,
            Permission.AdjustStocks,
            Permission.RelocateStocks,

            Permission.GetReceipts,
            Permission.ModifyReceipts,
            Permission.CreateReceipts,

            Permission.GetCustomers,
            Permission.ModifyCustomers,
            Permission.CreateCustomers,

            Permission.CreateCustomerContacts,
            Permission.ModifyCustomerContacts,

            Permission.CreateCustomerOutlets,
            Permission.ModifyCustomerOutlets,

            Permission.ModifyCustomerProducts,

            Permission.GetSuppliers,
            Permission.ModifySuppliers,
            Permission.CreateSuppliers,

            Permission.CreateSupplierContacts,
            Permission.ModifySupplierContacts,

            Permission.ModifySupplierProducts,

            Permission.GetPriceTiers,
            Permission.ModifyPriceTiers,

            Permission.CreatePurchaseOrders,
            Permission.GetPurchaseOrders,
            Permission.ModifyPurchaseOrders,

            Permission.ApprovePurchaseOrders,
            Permission.CancelPurchaseOrders,

            Permission.GetDrivers,
            Permission.ModifyDrivers,
            Permission.CreateDrivers,

            Permission.GetRoutes,
            Permission.ModifyRoutes,
            Permission.CreateRoutes,

            Permission.GetRouteTrips,
            Permission.ModifyRouteTrips,
            Permission.CreateRouteTrips,

            Permission.ModifyRouteTriplogs,
            Permission.ModifyOtherRouteTripLogs,

            Permission.GetPutAwayTasks,
            Permission.ModifyPutAwayTasks,
            Permission.ManagePutAwayTasks,
            Permission.ExecutePutAwayTasks,

            Permission.GetWarehouseOperators,
            Permission.ModifyWarehouseOperators,
            Permission.CreateWarehouseOperators,

            Permission.GetCart,
            Permission.AddToCart,
            Permission.RemoveFromCart);


        builder
            .HasMany<Role>()
            .WithMany()
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("role_permissions");

                joinBuilder.HasData(
                    // Driver permissions
                    CreateRolePermission(Role.Driver, Permission.GetRouteTrips),
                    CreateRolePermission(Role.Driver, Permission.ModifyRouteTriplogs),

                    // Warehouse Operator permissions
                    CreateRolePermission(Role.WarehouseOperator, Permission.GetPutAwayTasks),
                    CreateRolePermission(Role.WarehouseOperator, Permission.ExecutePutAwayTasks),

                    // Warehouse Manager permissions
                    CreateRolePermission(Role.WarehouseManager, Permission.GetPutAwayTasks),
                    CreateRolePermission(Role.WarehouseManager, Permission.ManagePutAwayTasks),

                    // Customer permissions
                    CreateRolePermission(Role.Customer, Permission.GetCart),
                    CreateRolePermission(Role.Customer, Permission.AddToCart),
                    CreateRolePermission(Role.Customer, Permission.RemoveFromCart),
                    CreateRolePermission(Role.Customer, Permission.GetOrders),
                    CreateRolePermission(Role.Customer, Permission.CreateOrder),

                    // Admin permissions
                    CreateRolePermission(Role.Administrator, Permission.GetUser),
                    CreateRolePermission(Role.Administrator, Permission.ModifyUser),

                    CreateRolePermission(Role.Administrator, Permission.GetEvents),
                    CreateRolePermission(Role.Administrator, Permission.SearchEvents),
                    CreateRolePermission(Role.Administrator, Permission.ModifyEvents),
                    CreateRolePermission(Role.Administrator, Permission.GetTicketTypes),
                    CreateRolePermission(Role.Administrator, Permission.ModifyTicketTypes),
                    CreateRolePermission(Role.Administrator, Permission.GetCategories),
                    CreateRolePermission(Role.Administrator, Permission.ModifyCategories),
                    CreateRolePermission(Role.Administrator, Permission.GetTickets),
                    CreateRolePermission(Role.Administrator, Permission.CheckInTicket),
                    CreateRolePermission(Role.Administrator, Permission.GetEventStatistics),

                    CreateRolePermission(Role.Administrator, Permission.GetProducts),
                    CreateRolePermission(Role.Administrator, Permission.CreateProducts),
                    CreateRolePermission(Role.Administrator, Permission.ModifyProducts),

                    CreateRolePermission(Role.Administrator, Permission.GetStocks),
                    CreateRolePermission(Role.Administrator, Permission.GetStockTransactions),
                    CreateRolePermission(Role.Administrator, Permission.AdjustStocks),
                    CreateRolePermission(Role.Administrator, Permission.RelocateStocks),

                    CreateRolePermission(Role.Administrator, Permission.GetReceipts),
                    CreateRolePermission(Role.Administrator, Permission.ModifyReceipts),
                    CreateRolePermission(Role.Administrator, Permission.CreateReceipts),

                    CreateRolePermission(Role.Administrator, Permission.GetCustomers),
                    CreateRolePermission(Role.Administrator, Permission.ModifyCustomers),
                    CreateRolePermission(Role.Administrator, Permission.CreateCustomers),

                    CreateRolePermission(Role.Administrator, Permission.CreateCustomerContacts),
                    CreateRolePermission(Role.Administrator, Permission.ModifyCustomerContacts),

                    CreateRolePermission(Role.Administrator, Permission.CreateCustomerOutlets),
                    CreateRolePermission(Role.Administrator, Permission.ModifyCustomerOutlets),

                    CreateRolePermission(Role.Administrator, Permission.ModifyCustomerProducts),

                    CreateRolePermission(Role.Administrator, Permission.GetSuppliers),
                    CreateRolePermission(Role.Administrator, Permission.ModifySuppliers),
                    CreateRolePermission(Role.Administrator, Permission.CreateSuppliers),

                    CreateRolePermission(Role.Administrator, Permission.CreateSupplierContacts),
                    CreateRolePermission(Role.Administrator, Permission.ModifySupplierContacts),

                    CreateRolePermission(Role.Administrator, Permission.ModifySupplierProducts),

                    CreateRolePermission(Role.Administrator, Permission.GetPriceTiers),
                    CreateRolePermission(Role.Administrator, Permission.ModifyPriceTiers),

                    CreateRolePermission(Role.Administrator, Permission.CreatePurchaseOrders),
                    CreateRolePermission(Role.Administrator, Permission.ModifyPurchaseOrders),
                    CreateRolePermission(Role.Administrator, Permission.GetPurchaseOrders),

                    CreateRolePermission(Role.Administrator, Permission.ApprovePurchaseOrders),
                    CreateRolePermission(Role.Administrator, Permission.CancelPurchaseOrders),

                    CreateRolePermission(Role.Administrator, Permission.GetDrivers),
                    CreateRolePermission(Role.Administrator, Permission.ModifyDrivers),
                    CreateRolePermission(Role.Administrator, Permission.CreateDrivers),

                    CreateRolePermission(Role.Administrator, Permission.GetRoutes),
                    CreateRolePermission(Role.Administrator, Permission.ModifyRoutes),
                    CreateRolePermission(Role.Administrator, Permission.CreateRoutes),

                    CreateRolePermission(Role.Administrator, Permission.GetRouteTrips),
                    CreateRolePermission(Role.Administrator, Permission.ModifyRouteTrips),
                    CreateRolePermission(Role.Administrator, Permission.CreateRouteTrips),

                    CreateRolePermission(Role.Administrator, Permission.ModifyRouteTriplogs),
                    CreateRolePermission(Role.Administrator, Permission.ModifyOtherRouteTripLogs),

                    CreateRolePermission(Role.Administrator, Permission.GetPutAwayTasks),
                    CreateRolePermission(Role.Administrator, Permission.ModifyPutAwayTasks),
                    CreateRolePermission(Role.Administrator, Permission.ManagePutAwayTasks),
                    CreateRolePermission(Role.Administrator, Permission.ExecutePutAwayTasks),
                    
                    CreateRolePermission(Role.Administrator, Permission.GetWarehouseOperators),
                    CreateRolePermission(Role.Administrator, Permission.ModifyWarehouseOperators),
                    CreateRolePermission(Role.Administrator, Permission.CreateWarehouseOperators),

                    CreateRolePermission(Role.Administrator, Permission.GetCart),
                    CreateRolePermission(Role.Administrator, Permission.AddToCart),
                    CreateRolePermission(Role.Administrator, Permission.RemoveFromCart));
            });
    }

    private static object CreateRolePermission(Role role, Permission permission)
    {
        return new
        {
            RoleName = role.Name,
            PermissionCode = permission.Code
        };
    }
}
