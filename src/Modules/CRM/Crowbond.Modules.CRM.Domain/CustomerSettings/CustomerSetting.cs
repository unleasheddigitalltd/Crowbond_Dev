using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerSettings;

public sealed class CustomerSetting : Entity
{
    private CustomerSetting()
    {        
    }

    public Guid Id { get; private set; }

    public Guid CustomerId { get; private set; }

    public bool ShowPricesInDeliveryDocket { get; private set; }

    public bool ShowPriceInApp { get; private set; }

    public ShowLogoInDeliveryDocket ShowLogoInDeliveryDocket {  get; private set; }

    public string? CustomerLogo { get; private set; }

    internal static CustomerSetting Create(
        Guid customerId,
        bool showPricesInDeliveryDocket,
        bool showPriceInApp,
        ShowLogoInDeliveryDocket showLogoInDeliveryDocket)
    {
        var customerSetting = new CustomerSetting
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            ShowPricesInDeliveryDocket = showPricesInDeliveryDocket,
            ShowPriceInApp = showPriceInApp,
            ShowLogoInDeliveryDocket = showLogoInDeliveryDocket
        };

        return customerSetting;
    }

    internal void Update(
        bool showPricesInDeliveryDocket,
        bool showPriceInApp,
        ShowLogoInDeliveryDocket showLogoInDeliveryDocket)
    {
        ShowPricesInDeliveryDocket = showPricesInDeliveryDocket;
        ShowPriceInApp = showPriceInApp;
        ShowLogoInDeliveryDocket = showLogoInDeliveryDocket;
    }

    internal void SetLogo(string customerLogo)
    {
        CustomerLogo = customerLogo;
    }

    internal void RemoveLogo()
    {
        CustomerLogo = null;
    }

}
