using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Events.Application.Categories.ArchiveCategory;

public sealed record ArchiveCategoryCommand(Guid CategoryId) : ICommand;
