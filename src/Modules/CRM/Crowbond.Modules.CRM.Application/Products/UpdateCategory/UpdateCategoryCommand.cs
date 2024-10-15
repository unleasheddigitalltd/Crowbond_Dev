using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Products.UpdateCategory;

public sealed record UpdateCategoryCommand(Guid Id, string Name) : ICommand;
