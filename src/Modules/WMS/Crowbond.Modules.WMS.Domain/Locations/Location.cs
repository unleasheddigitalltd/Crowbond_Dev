using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Tasks;
using Serilog.Parsing;

namespace Crowbond.Modules.WMS.Domain.Locations;

public sealed class Location : Entity
{
    private Location()
    {
    }

    public Guid Id { get; private set; }

    public Guid? ParentId { get; private set; }

    public string Name { get; private set; }

    public string? ScanCode { get; private set; }
    public string? NetworkAddress { get; private set; }
    public string? PrinterName { get; private set; }

    public LocationType? LocationType { get; private set; }

    public LocationLayer LocationLayer { get; private set; }

    public LocationStatus Status { get; private set; }

    public static Result<Location> Create(
        Guid? parentId,
        string name,
        string? scanCode,
        string? networkAddress,
        string? printerName,
        LocationType? locationType,
        LocationLayer locationLayer)
    {
        if (locationLayer != LocationLayer.Location && locationType != null)
        {
            return Result.Failure<Location>(LocationErrors.InvalidLocationTypeAssignment);
        }

        if (parentId is null &&(locationLayer == LocationLayer.Area || locationLayer == LocationLayer.Location))
        {
            return Result.Failure<Location>(LocationErrors.NeedParent);            
        }

        if (locationLayer == LocationLayer.Site && parentId is not null)
        {
            return Result.Failure<Location>(LocationErrors.CanNotHaveParent);
        }

        var location = new Location
        {
            Id = Guid.NewGuid(),
            ParentId = parentId,
            Name = name,
            ScanCode = scanCode,
            NetworkAddress = networkAddress,
            PrinterName = printerName,
            LocationType = locationType,
            LocationLayer = locationLayer,
            Status = LocationStatus.Active
        };

        return location;
    }

    public Result Update(
        Guid? parentId,
        string name,
        string? scanCode,
        string? networkAddress,
        string? printerName,
        LocationType? locationType,
        LocationLayer locationLayer)
    {
        if (locationLayer != LocationLayer.Location && locationType != null)
        {
            return Result.Failure(LocationErrors.InvalidLocationTypeAssignment);
        }

        if (parentId is null && (locationLayer == LocationLayer.Area || locationLayer == LocationLayer.Location))
        {
            return Result.Failure(LocationErrors.NeedParent);
        }

        if (locationLayer == LocationLayer.Site)
        {
            return Result.Failure(LocationErrors.CanNotHaveParent);
        }

        ParentId = parentId;
        Name = name;
        ScanCode = scanCode;
        NetworkAddress = networkAddress;
        PrinterName = printerName;
        LocationType = locationType;
        LocationLayer = locationLayer;

        return Result.Success();
    }

    public Result Activate()
    {
        if (Status == LocationStatus.Active)
        {
            return Result.Failure(LocationErrors.IsAlreadyActive(Id));
        }

        Status = LocationStatus.Active;

        return Result.Success();
    }

    public Result Hold()
    {
        if (Status == LocationStatus.Held)
        {
            return Result.Failure(LocationErrors.IsAlreadyHeld(Id));
        }

        Status = LocationStatus.Held;

        return Result.Success();
    }
}
