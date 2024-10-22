using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Locations.GetLocationByScanCode;

public sealed record GetLocationByScanCodeQuery(string ScanCode) : IQuery<LocationResponse>;
