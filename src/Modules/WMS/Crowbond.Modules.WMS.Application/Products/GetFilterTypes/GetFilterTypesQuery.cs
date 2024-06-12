﻿using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Products.GetFilterTypes.Dtos;

namespace Crowbond.Modules.WMS.Application.Products.GetFilterTypes;

public sealed record GetFilterTypesQuery() : IQuery<IReadOnlyCollection<FilterTypeResponse>>;
