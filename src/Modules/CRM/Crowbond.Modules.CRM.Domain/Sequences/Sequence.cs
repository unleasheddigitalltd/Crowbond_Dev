using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Sequences;

public sealed class Sequence : Entity
{


    public Sequence()
    {

    }

    public Guid Id { get; private set; }
    public SequenceContext Context { get; private set; }
    public int LastNumber { get; private set; }

}
