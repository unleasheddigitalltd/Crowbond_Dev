using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Application.Clock.ClearFixedCurrentDateTime;

internal sealed class ClearFixedCurrentDateTimeCommandHandler(IDateTimeProvider dateTimeProvider)
    : ICommandHandler<ClearFixedCurrentDateTimeCommand>
{
    public async Task<Result> Handle(ClearFixedCurrentDateTimeCommand request, CancellationToken cancellationToken)
    {
        await dateTimeProvider.ClearOverrideUtcNow();
        return Result.Success();
    }
}
