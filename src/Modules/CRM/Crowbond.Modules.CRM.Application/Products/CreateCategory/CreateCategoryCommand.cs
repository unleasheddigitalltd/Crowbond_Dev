using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Products.CreateCategory;

public sealed record CreateCategoryCommand(Guid Id, string Name) : ICommand;
