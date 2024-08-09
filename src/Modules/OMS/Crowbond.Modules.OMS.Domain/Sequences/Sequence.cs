using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Sequences;

public sealed class Sequence : Entity
{
    private Sequence()
    {
    }

    public Guid Id { get; private set; }
    public SequenceContext Context { get; private set; }
    public int LastNumber { get; private set; }

    public void IncreaseSequence()
    {
        LastNumber++;
    }

    public Sequence Create(SequenceContext context, int lastNumber)
    {
        var sequence = new Sequence
        {
            Id = Guid.NewGuid(),
            Context = context,
            LastNumber = lastNumber
        };

        return sequence;
    }
}
