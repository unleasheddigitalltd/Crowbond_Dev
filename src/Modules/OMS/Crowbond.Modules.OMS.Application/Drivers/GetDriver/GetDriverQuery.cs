using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Drivers.GetDriver;

public sealed record GetDriverQuery(Guid DriverId) : IQuery<DriverResponse>;
