using System.Reflection;
using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerContacts;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.UpdateCustomerContact;

internal sealed class UpdateCustomerContactCommandHandler(
    ICustomerContactRepository customerContactRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCustomerContactCommand>
{
    public async Task<Result> Handle(UpdateCustomerContactCommand request, CancellationToken cancellationToken)
    {
        CustomerContact? customerContact = await customerContactRepository.GetAsync(request.CustomerContactId, cancellationToken);

        if (customerContact == null)
        {
            return Result.Failure(CustomerContactErrors.NotFound(request.CustomerContactId));
        }

        customerContact.Update(
        firstName: request.CustomerContact.FirstName,
        lastName: request.CustomerContact.LastName,
        phoneNumber: request.CustomerContact.PhoneNumber,
        mobile: request.CustomerContact.Mobile,
        receiveInvoice: request.CustomerContact.ReceiveInvoice,
        receiveOrder: request.CustomerContact.ReceiveOrder,
        receivePriceList: request.CustomerContact.ReceivePriceList,
        lastModifiedBy: request.UserId,
        lastModifiedDate: dateTimeProvider.UtcNow);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
