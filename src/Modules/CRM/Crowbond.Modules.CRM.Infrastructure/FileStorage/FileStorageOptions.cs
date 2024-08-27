namespace Crowbond.Modules.CRM.Infrastructure.FileStorage;

public sealed class FileStorageOptions
{
    public string FtpHost { get; init; }
    public string FtpUsername { get; init; }
    public string FtpPassword { get; init; }
    public string CustomerLogoStoragePath { get; init; }
}
