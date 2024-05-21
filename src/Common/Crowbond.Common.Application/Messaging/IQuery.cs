using Crowbond.Common.Domain;
using MediatR;

namespace Crowbond.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
