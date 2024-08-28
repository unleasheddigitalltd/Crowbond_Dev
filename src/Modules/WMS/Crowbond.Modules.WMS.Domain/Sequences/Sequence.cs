using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Sequences;

public sealed class Sequence
{
    public static readonly Sequence Receipt = new (SequenceContext.Receipt, 10001);
    public static readonly Sequence Task = new(SequenceContext.Task, 10001);

    private Sequence()
    {
    }

    private Sequence(SequenceContext context, int lastNumber)
    {
        Context = context;
        LastNumber = lastNumber;        
    }

    public SequenceContext Context { get; private set; }
    public int LastNumber { get; private set; }

    public int GetNewSequence()
    {
        LastNumber++;
        return LastNumber;
    }
}
