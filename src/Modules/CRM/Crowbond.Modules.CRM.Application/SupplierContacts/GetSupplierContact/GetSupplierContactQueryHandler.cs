using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.CustomerContacts.GetCustomerContactDetails;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Dapper;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.GetSupplierContact;

internal sealed class GetSupplierContactQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSupplierContactQuery, SupplierContactResponse>
{
    public async Task<Result<SupplierContactResponse>> Handle(GetSupplierContactQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(SupplierContactResponse.Id)},
                 supplier_id AS {nameof(SupplierContactResponse.SupplierId)},
                 first_name AS {nameof(CustomerContactDetailsResponse.FirstName)},
                 last_name AS {nameof(CustomerContactDetailsResponse.LastName)},
                 phone_number AS {nameof(CustomerContactDetailsResponse.PhoneNumber)},
                 mobile AS {nameof(CustomerContactDetailsResponse.Mobile)},
                 username AS {nameof(CustomerContactDetailsResponse.Username)},
                 email AS {nameof(CustomerContactDetailsResponse.Email)},
                 primary AS {nameof(CustomerContactDetailsResponse.Primary)},             
                 receive_invoice AS {nameof(CustomerContactDetailsResponse.ReceiveInvoice)}, 
                 receive_order AS {nameof(CustomerContactDetailsResponse.ReceiveOrder)}, 
                 receive_price_list AS {nameof(CustomerContactDetailsResponse.ReceivePriceList)},
                 is_active AS {nameof(CustomerContactDetailsResponse.IsActive)}
             FROM crm.supplier_contacts t
             WHERE id = @supplierContactId
             """;

        SupplierContactResponse? supplierContact = await connection.QuerySingleOrDefaultAsync<SupplierContactResponse>(sql, request);

        if (supplierContact is null)
        {
            return Result.Failure<SupplierContactResponse>(CustomerContactErrors.NotFound(request.SupplierContactId));
        }

        return supplierContact;
    }
}
