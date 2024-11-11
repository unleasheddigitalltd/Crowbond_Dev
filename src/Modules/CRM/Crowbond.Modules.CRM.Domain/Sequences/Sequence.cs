using System.Globalization;
using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Sequences;

public sealed class Sequence : Entity
{
    public static readonly Sequence Customer = new(SequenceContext.Customer, 5, 10001, "CUS");
    public static readonly Sequence Supplier = new(SequenceContext.Supplier, 5, 10001, "SUP");


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
        string formattedNumber = LastNumber.ToString($"D{Length}", CultureInfo.InvariantCulture);
        return $"{Prefix}-{formattedNumber}";
    }
}
