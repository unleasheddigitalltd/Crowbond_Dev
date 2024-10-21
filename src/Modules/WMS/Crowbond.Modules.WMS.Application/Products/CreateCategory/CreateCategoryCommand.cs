using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : ICommand<Guid>;
