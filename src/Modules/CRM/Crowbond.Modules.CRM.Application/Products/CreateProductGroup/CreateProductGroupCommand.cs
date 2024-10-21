using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Products.CreateProductGroup;

public sealed record CreateProductGroupCommand(Guid Id, string Name) : ICommand;
