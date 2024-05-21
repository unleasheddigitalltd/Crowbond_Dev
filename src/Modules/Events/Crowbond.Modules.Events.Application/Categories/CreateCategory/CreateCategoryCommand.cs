using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Events.Application.Categories.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : ICommand<Guid>;
