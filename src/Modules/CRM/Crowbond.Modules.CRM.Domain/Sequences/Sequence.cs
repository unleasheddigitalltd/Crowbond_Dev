using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Sequences;

public sealed class Sequence : Entity
{
    public static readonly Sequence Customer = new(SequenceContext.Customer, 10001, "CUS");
    public static readonly Sequence Supplier = new(SequenceContext.Supplier, 10001, "SUP");


    private Sequence(SequenceContext context, int lastNumber, string prefix)
    {
        Context = context;
        LastNumber = lastNumber;
        Prefix = prefix;
    }

    private Sequence() { }

    public SequenceContext Context { get; private set; }
    public int LastNumber { get; private set; }
    public string Prefix { get; private set; }

    public string GetNumber()
    {
        LastNumber++;
        return $"{Prefix}-{LastNumber}";
    }
}
