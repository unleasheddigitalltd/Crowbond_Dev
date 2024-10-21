using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.UpdateCategory;

public sealed record UpdateCategoryCommand(Guid Id, string Name) : ICommand;
