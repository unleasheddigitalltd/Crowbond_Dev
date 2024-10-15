namespace Crowbond.Modules.OMS.Infrastructure.FileStorage;

public sealed class FileStorageOptions
{
    public string FtpHost { get; init; }
    public string FtpUsername { get; init; }
    public string FtpPassword { get; init; }
    public string OrderDeliveryImageStoragePath { get; init; }
    public int MaxFileSizeKb { get; init; }
}
