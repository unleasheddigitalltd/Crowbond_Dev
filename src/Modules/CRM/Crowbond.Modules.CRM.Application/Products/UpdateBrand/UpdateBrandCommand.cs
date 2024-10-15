using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Products.UpdateBrand;

public sealed record UpdateBrandCommand(Guid Id, string Name) : ICommand;
