namespace Crowbond.Modules.CRM.PublicApi;

public interface ICustomerContactApi
{
    Task<CustomerContactResponse?> GetAsync(Guid contactId, CancellationToken cancellationToken = default);
}

public sealed record CustomerContactResponse(
    Guid Id,
    Guid CustomerId);
