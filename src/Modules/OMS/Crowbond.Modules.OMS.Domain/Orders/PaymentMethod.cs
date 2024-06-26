namespace Crowbond.Modules.OMS.Domain.Orders;
public sealed class PaymentMethod
{
    public static readonly PaymentMethod Unpaid = new("Unpaid");
    public static readonly PaymentMethod Paid = new("Paid");
    

    private PaymentMethod(string name)
    {
        Name = name;
    }

    public PaymentMethod()
    {
    }
    public string Name { get; private set; }
}
