﻿using Crowbond.Common.Domain;
using MediatR;

namespace Crowbond.Common.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
