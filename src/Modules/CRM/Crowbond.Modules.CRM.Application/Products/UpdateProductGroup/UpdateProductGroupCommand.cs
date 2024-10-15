using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Products.UpdateProductGroup;

public sealed record UpdateProductGroupCommand(Guid Id, string Name) : ICommand;
