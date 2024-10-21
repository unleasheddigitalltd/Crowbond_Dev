using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Products.CreateBrand;

public sealed record CreateBrandCommand(Guid Id, string Name) : ICommand;
