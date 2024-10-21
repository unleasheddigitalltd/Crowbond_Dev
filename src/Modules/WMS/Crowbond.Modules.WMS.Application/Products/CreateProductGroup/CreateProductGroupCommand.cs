using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.CreateProductGroup;

public sealed record CreateProductGroupCommand(string Name) : ICommand<Guid>;
