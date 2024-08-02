using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Reps.GetReps;

public sealed record GetRepsQuery() : IQuery<IReadOnlyCollection<RepResponse>>;
