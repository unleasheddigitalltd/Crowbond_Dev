namespace Crowbond.Common.Application.Authentication;
public interface ICurrentUserContext
{
    Guid UserId { get; }
}
