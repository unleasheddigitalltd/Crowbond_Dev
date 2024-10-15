using Crowbond.Common.Application.Messaging;
using Microsoft.AspNetCore.Http;

namespace Crowbond.Modules.OMS.Application.Orders.UploadOrderDeliveryImage;

public sealed record UploadOrderDeliveryImageCommand(Guid OrderHeaderId, IFormFile Image) : ICommand;
