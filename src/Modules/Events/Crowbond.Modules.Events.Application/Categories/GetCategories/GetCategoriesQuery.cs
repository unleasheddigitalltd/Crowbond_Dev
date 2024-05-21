using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Events.Application.Categories.GetCategory;

namespace Crowbond.Modules.Events.Application.Categories.GetCategories;

public sealed record GetCategoriesQuery : IQuery<IReadOnlyCollection<CategoryResponse>>;
