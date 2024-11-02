using System.Globalization;
using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Sequences;

public sealed class Sequence : Entity
{
    public static readonly Sequence Receipt = new (SequenceContext.Receipt, 10001, 5, "RCP");
    public static readonly Sequence Task = new(SequenceContext.Task, 10001, 5, "TSK");
    public static readonly Sequence Dispatch = new(SequenceContext.Dispatch, 10001, 5, "DSP");
    public static readonly Sequence Tote = new(SequenceContext.Tote, 0, 3, "TOT");
    public static readonly Sequence Pallet = new(SequenceContext.Pallet, 0, 3, "PLT");


    private Sequence(SequenceContext context, int lastNumber, int length, string prefix)
    {
        Context = context;
        LastNumber = lastNumber;
        Length = length;
        Prefix = prefix;
    }

    private Sequence() {}

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
