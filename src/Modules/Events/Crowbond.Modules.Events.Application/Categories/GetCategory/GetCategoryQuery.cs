using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Events.Application.Categories.GetCategory;

public sealed record GetCategoryQuery(Guid CategoryId) : IQuery<CategoryResponse>;
