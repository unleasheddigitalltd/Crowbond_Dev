namespace Crowbond.Modules.OMS.Domain.PurchaseOrders;
public sealed class PaymentMethod
{
    public static readonly PaymentMethod BankTransfer = new("Bank Transfer");
    public static readonly PaymentMethod CashOnDelivery = new("COD");
    public static readonly PaymentMethod CreditCard = new("CreditCard");
    public static readonly PaymentMethod Invoice = new("Invoice");


    private PaymentMethod(string name)
    {
        Name = name;
    }

    public PaymentMethod()
    {
    }
    public string Name { get; private set; }
}
