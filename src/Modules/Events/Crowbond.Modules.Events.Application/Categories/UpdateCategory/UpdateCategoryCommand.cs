using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Events.Application.Categories.UpdateCategory;

public sealed record UpdateCategoryCommand(Guid CategoryId, string Name) : ICommand;
