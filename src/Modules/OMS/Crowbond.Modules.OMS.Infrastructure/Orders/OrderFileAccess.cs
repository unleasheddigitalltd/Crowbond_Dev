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
        string directoryPath = _options.OrderDeliveryImageStoragePath;

        if (await ftpClient.DirectoryExists(directoryPath, cancellationToken))
        {
            FtpListItem[] files = await ftpClient.GetListing(directoryPath, FtpListOption.AllFiles, cancellationToken);

            foreach (FtpListItem file in files)
            {
                if (Path.GetFileName(file.Name).Equals(imageName, StringComparison.OrdinalIgnoreCase))
                {
                    string filePath = Path.Combine(_options.OrderDeliveryImageStoragePath, file.Name).Replace("\\", "/");
                    await ftpClient.DeleteFile(filePath, cancellationToken);
                }
            }
        }

        await ftpClient.Disconnect(cancellationToken);
    }

    public async Task<string> SaveDeliveryImageAsync(string orderNo, int LastSequence, IFormFile image, CancellationToken cancellationToken = default)
    {
        if (image == null)
        {
            throw new ArgumentException("No file uploaded.");
        }

        if (image.Length > _options.MaxFileSizeKb * 1024)
        {
            throw new ArgumentException($"File size exceeds the {_options.MaxFileSizeKb} KB limit.");
        }

        string imageFileName;
        int sequence = LastSequence + 1;
        using (var ftpClient = new AsyncFtpClient(_options.FtpHost, new NetworkCredential(_options.FtpUsername, _options.FtpPassword)))
        {
            await ftpClient.Connect(cancellationToken);

            string directoryPath = _options.OrderDeliveryImageStoragePath;

            if (!await ftpClient.DirectoryExists(directoryPath, cancellationToken))
            {
                await ftpClient.CreateDirectory(directoryPath, cancellationToken);
            }

            FtpListItem[] files = await ftpClient.GetListing(directoryPath, FtpListOption.AllFiles, cancellationToken);

            foreach (FtpListItem file in files)
            {
                if (Path.GetFileNameWithoutExtension(file.Name).Equals(orderNo, StringComparison.OrdinalIgnoreCase))
                {
                    string filePath = Path.Combine(_options.OrderDeliveryImageStoragePath, file.Name).Replace("\\", "/");
                    await ftpClient.DeleteFile(filePath, cancellationToken);
                }
            }

            imageFileName = $"{orderNo}-{sequence}{Path.GetExtension(image.FileName)}";
            using Stream stream = image.OpenReadStream();
            await ftpClient.UploadStream(
                stream,
                Path.Combine(_options.OrderDeliveryImageStoragePath, imageFileName),
                FtpRemoteExists.Overwrite, true, null, cancellationToken);

            await ftpClient.Disconnect(cancellationToken);
        }

        return imageFileName;
    }
}
