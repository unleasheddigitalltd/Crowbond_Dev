using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Application.Clock.SetFixedCurrentDateTime;

internal sealed class SetFixedCurrentDateTimeCommandHandler(IDateTimeProvider dateTimeProvider)
    : ICommandHandler<SetFixedCurrentDateTimeCommand>
{
    public async Task<Result> Handle(SetFixedCurrentDateTimeCommand request, CancellationToken cancellationToken)
    {
        await dateTimeProvider.SetOverrideUtcNow(request.CurrentDateTime);
        return Result.Success();
    }
}
