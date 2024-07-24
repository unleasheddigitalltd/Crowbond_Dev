namespace Crowbond.Modules.OMS.Application.Drivers.GetDriver;

public sealed record DriverResponse(Guid Id, string FirstName, string LastName, string Username, string Email, string Mobile, string? VehicleRegn);
