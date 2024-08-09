using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Suppliers.GetSupplier;
using Crowbond.Modules.CRM.PublicApi;
using MediatR;
using SupplierResponse = Crowbond.Modules.CRM.PublicApi.SupplierResponse;

namespace Crowbond.Modules.CRM.Infrastructure.PublicApi;

internal sealed class SupplierApi(ISender sender) : ISupplierApi
{
    public async Task<SupplierResponse?> GetAsync(Guid supplierId, CancellationToken cancellationToken = default)
    {
        Result<Application.Suppliers.GetSupplier.SupplierResponse> result =
            await sender.Send(new GetSupplierQuery(supplierId), cancellationToken);

        if (result.IsFailure)
        {
            return null;
        }

        return new SupplierResponse(
            result.Value.Id,
            result.Value.AccountNumber,
            result.Value.SupplierName,
            result.Value.AddressLine1,
            result.Value.AddressLine2,
            result.Value.TownCity,
            result.Value.County,
            result.Value.Country,
            result.Value.PostalCode,
            result.Value.FirstName,
            result.Value.LastName,
            result.Value.PhoneNumber,
            result.Value.Email);
    }
}
