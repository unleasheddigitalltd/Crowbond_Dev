using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.CreateBrand;

public sealed record CreateBrandCommand(string Name) : ICommand<Guid>;
