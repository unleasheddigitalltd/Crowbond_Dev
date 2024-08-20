using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.CreateCustomerContact;

internal sealed class CreateCustomerContactCommandHandler(
    ICustomerRepository customerRepository,
    ICustomerContactRepository customerContactRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCustomerContactCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCustomerContactCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetAsync(request.CustomerId, cancellationToken);

        if (customer == null)
        {
            return Result.Failure<Guid>(CustomerErrors.NotFound(request.CustomerId));
        }

        Result<CustomerContact> result = CustomerContact.Create(
            customerId: request.CustomerId,
            firstName: request.CustomerContact.FirstName,
            lastName: request.CustomerContact.LastName,
            phoneNumber: request.CustomerContact.PhoneNumber,
            mobile: request.CustomerContact.Mobile,
            email: request.CustomerContact.Email,
            username: request.CustomerContact.Username,
            receiveInvoice: request.CustomerContact.ReceiveInvoice,
            receiveOrder: request.CustomerContact.ReceiveOrder,
            receivePriceList: request.CustomerContact.ReceivePriceList,
            createBy: request.UserId,
            createDate: dateTimeProvider.UtcNow);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        customerContactRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
