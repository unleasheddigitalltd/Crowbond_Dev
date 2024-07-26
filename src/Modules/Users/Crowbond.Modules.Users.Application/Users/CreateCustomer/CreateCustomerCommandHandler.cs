using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Abstractions.Data;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Crowbond.Modules.Users.Domain.Users;

namespace Crowbond.Modules.Users.Application.Users.CreateCustomer;

internal sealed class CreateCustomerCommandHandler(
    IIdentityProviderService identityProviderService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCustomerCommand>
{
    public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        const  string defaultPassword = "123456";

        Result<string> result = await identityProviderService.RegisterUserAsync(
            new UserModel(request.Username, request.Email, defaultPassword, request.FirstName, request.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        var user = User.Create(request.UserId, request.Username, request.Email, request.FirstName, request.LastName, result.Value);

        user.AddRole(Role.Customer);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
