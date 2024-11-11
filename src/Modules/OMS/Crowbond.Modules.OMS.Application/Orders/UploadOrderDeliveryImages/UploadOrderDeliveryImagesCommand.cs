using Crowbond.Common.Application.Messaging;
using Microsoft.AspNetCore.Http;

namespace Crowbond.Modules.OMS.Application.Orders.UploadOrderDeliveryImages;
public sealed record UploadOrderDeliveryImagesCommand(Guid OrderHeaderId, IFormFileCollection Images) : ICommand;
