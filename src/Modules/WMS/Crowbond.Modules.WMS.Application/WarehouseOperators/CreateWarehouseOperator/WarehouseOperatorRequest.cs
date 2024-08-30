namespace Crowbond.Modules.WMS.Application.WarehouseOperators.CreateWarehouseOperator;

public sealed record WarehouseOperatorRequest(
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string Mobile);
