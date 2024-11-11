using System.Net;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Infrastructure.FileStorage;
using FluentFTP;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Crowbond.Modules.OMS.Infrastructure.Orders;

internal sealed class OrderFileAccess(IOptions<FileStorageOptions> options) : IOrderFileAccess
{
    private readonly FileStorageOptions _options = options.Value;
    public async Task DeleteDeliveryImageAsync(string imageName, CancellationToken cancellationToken = default)
    {
        using var ftpClient = new AsyncFtpClient(_options.FtpHost, new NetworkCredential(_options.FtpUsername, _options.FtpPassword));
        await ftpClient.Connect(cancellationToken);
        string directoryPath = _options.DeliveryStoragePath;

        if (await ftpClient.DirectoryExists(directoryPath, cancellationToken))
        {
            FtpListItem[] files = await ftpClient.GetListing(directoryPath, FtpListOption.AllFiles, cancellationToken);

            foreach (FtpListItem file in files)
            {
                if (Path.GetFileName(file.Name).Equals(imageName, StringComparison.OrdinalIgnoreCase))
                {
                    string filePath = Path.Combine(_options.DeliveryStoragePath, file.Name).Replace("\\", "/");
                    await ftpClient.DeleteFile(filePath, cancellationToken);
                }
            }
        }

        await ftpClient.Disconnect(cancellationToken);
    }

    public async Task<List<string>> SaveDeliveryImagesAsync(
    string orderNo,
    int lastSequence,
    IFormFileCollection images,
    CancellationToken cancellationToken = default)
    {
        var savedImages = new List<string>();
        using (var ftpClient = new AsyncFtpClient(_options.FtpHost, new NetworkCredential(_options.FtpUsername, _options.FtpPassword)))
        {
            await ftpClient.Connect(cancellationToken);

            string directoryPath = _options.DeliveryStoragePath;

            if (!await ftpClient.DirectoryExists(directoryPath, cancellationToken))
            {
                await ftpClient.CreateDirectory(directoryPath, cancellationToken);
            }

            int sequence = lastSequence;
            foreach (IFormFile image in images)
            {
                sequence++;
                string imageFileName = $"{orderNo}-{sequence}{Path.GetExtension(image.FileName)}";

                using Stream stream = image.OpenReadStream();
                await ftpClient.UploadStream(
                    stream,
                    Path.Combine(directoryPath, imageFileName),
                    FtpRemoteExists.Overwrite, true, null, cancellationToken);

                savedImages.Add(imageFileName);
            }


            await ftpClient.Disconnect(cancellationToken);
        }

        return savedImages;
    }
}
