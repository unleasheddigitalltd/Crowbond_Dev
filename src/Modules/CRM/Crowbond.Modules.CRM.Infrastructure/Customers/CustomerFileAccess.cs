using System.Net;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Infrastructure.FileStorage;
using FluentFTP;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Crowbond.Modules.CRM.Infrastructure.Customers;

internal sealed class CustomerFileAccess(IOptions<FileStorageOptions> options) : ICustomerFileAccess
{
    private readonly FileStorageOptions _options = options.Value;
    public async Task DeleteLogoAsync(string accountNumber, CancellationToken cancellationToken = default)
    {
        using var ftpClient = new AsyncFtpClient(_options.FtpHost, new NetworkCredential(_options.FtpUsername, _options.FtpPassword));
        await ftpClient.Connect(cancellationToken);

        string directoryPath = _options.CustomerLogoStoragePath;

        if (await ftpClient.DirectoryExists(directoryPath, cancellationToken))
        {
            FtpListItem[] files = await ftpClient.GetListing(directoryPath, FtpListOption.AllFiles, cancellationToken);

            foreach (FtpListItem file in files)
            {
                if (Path.GetFileNameWithoutExtension(file.Name).Equals(accountNumber, StringComparison.OrdinalIgnoreCase))
                {
                    string filePath = Path.Combine(_options.CustomerLogoStoragePath, file.Name).Replace("\\", "/");
                    await ftpClient.DeleteFile(filePath, cancellationToken);
                }
            }
        }

        await ftpClient.Disconnect(cancellationToken);
    }

    public async Task<string> SaveLogoAsync(string accountNumber, IFormFile logo, CancellationToken cancellationToken = default)
    {
        if (logo == null || logo.Length == 0)
        {
            throw new ArgumentException("No file uploaded.");
        }

        string logoFileName = $"{accountNumber}{Path.GetExtension(logo.FileName)}";

        using (var ftpClient = new AsyncFtpClient(_options.FtpHost, new NetworkCredential(_options.FtpUsername, _options.FtpPassword)))
        {
            await ftpClient.Connect(cancellationToken);

            string directoryPath = _options.CustomerLogoStoragePath;

            if (!await ftpClient.DirectoryExists(directoryPath, cancellationToken))
            {
                await ftpClient.CreateDirectory(directoryPath, cancellationToken);
            }

            FtpListItem[] files = await ftpClient.GetListing(directoryPath, FtpListOption.AllFiles, cancellationToken);

            foreach (FtpListItem file in files)
            {
                if (Path.GetFileNameWithoutExtension(file.Name).Equals(accountNumber, StringComparison.OrdinalIgnoreCase))
                {
                    string filePath = Path.Combine(_options.CustomerLogoStoragePath, file.Name).Replace("\\", "/");
                    await ftpClient.DeleteFile(filePath, cancellationToken);
                }
            }

            using (Stream stream = logo.OpenReadStream())
            {
                await ftpClient.UploadStream(
                    stream, 
                    Path.Combine(_options.CustomerLogoStoragePath, logoFileName),
                    FtpRemoteExists.Overwrite, true, null, cancellationToken);
            }

            await ftpClient.Disconnect(cancellationToken);
        }

        return logoFileName;
    }
}
