using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.UpdateProductGroup;

public sealed record UpdateProductGroupCommand(Guid Id, string Name) : ICommand;
