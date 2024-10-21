using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Sequences;

public sealed class Sequence : Entity
{
    public static readonly Sequence Receipt = new (SequenceContext.Receipt, 10001, "RCP");
    public static readonly Sequence Task = new(SequenceContext.Task, 10001, "TSK");
    public static readonly Sequence Dispatch = new(SequenceContext.Dispatch, 10001, "DSP");


    private Sequence(SequenceContext context, int lastNumber, string prefix)
    {
        Context = context;
        LastNumber = lastNumber;
        Prefix = prefix;
    }

    private Sequence() {}

    public SequenceContext Context { get; private set; }
    public int LastNumber { get; private set; }
    public string Prefix { get; private set; }

    public string GetNumber()
    {
        LastNumber++;
        return $"{Prefix}-{LastNumber}";
    }
}
