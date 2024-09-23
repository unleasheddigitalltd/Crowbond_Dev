using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Sequences;

public sealed class Sequence : Entity
{
    public static readonly Sequence Invoice = new(SequenceContext.Invoice, 10001, "INV");
    public static readonly Sequence PurchaseOrder = new(SequenceContext.PurchaseOrder, 10001, "POR");
    public static readonly Sequence Order = new(SequenceContext.Order, 10001, "SOR");


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
