using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.CustomerContacts.GetCustomerContact;
using Crowbond.Modules.CRM.PublicApi;
using MediatR;
using CustomerContactResponse = Crowbond.Modules.CRM.PublicApi.CustomerContactResponse;

namespace Crowbond.Modules.CRM.Infrastructure.PublicApi;

internal sealed class CustomerApi(ISender sender) : ICustomerContactApi
{
    public async Task<CustomerContactResponse?> GetAsync(Guid contactId, CancellationToken cancellationToken = default)
    {
        Result<Application.CustomerContacts.GetCustomerContact.CustomerContactResponse> result =
            await sender.Send(new GetCustomerContactQuery(contactId), cancellationToken);

        if (result.IsFailure)
        {
            return null;
        }

        return new CustomerContactResponse(
            result.Value.Id,
            result.Value.CustomerId);
    }
}
