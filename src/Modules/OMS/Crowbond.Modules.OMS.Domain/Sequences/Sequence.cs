using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Sequences;

public sealed class Sequence : Entity
{
    public static readonly Sequence Invoice = new(SequenceContext.Invoice, 10001, 5, "INV");
    public static readonly Sequence PurchaseOrder = new(SequenceContext.PurchaseOrder, 5, 10001, "POR");
    public static readonly Sequence Order = new(SequenceContext.Order, 10001, 5, "SOR");
    public static readonly Sequence Compliance = new(SequenceContext.Compliance, 10001, 5, "CMP");


    private Sequence(SequenceContext context, int lastNumber, int length, string prefix)
    {
        Context = context;
        LastNumber = lastNumber;
        Length = length;
        Prefix = prefix;
    }

    private Sequence() { }

    public SequenceContext Context { get; private set; }
    public int LastNumber { get; private set; }
    public int Length { get; private set; }
    public string Prefix { get; private set; }

    public string GetNumber()
    {
        int maxValue = (int)Math.Pow(10, Length) - 1;

        if (LastNumber > maxValue)
        {
            throw new InvalidOperationException($"The last number {LastNumber} of context {Context} exceeds the maximum allowable value for length {Length}.");
        }

        LastNumber++;
        return $"{Prefix}-{LastNumber}:D{Length}";
    }
}
