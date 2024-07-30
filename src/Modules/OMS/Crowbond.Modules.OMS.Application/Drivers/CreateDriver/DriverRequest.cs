namespace Crowbond.Modules.OMS.Application.Drivers.CreateDriver;

public sealed record DriverRequest(
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string Mobile,
    string? VehicleRegn);
