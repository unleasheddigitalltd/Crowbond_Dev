namespace Crowbond.Modules.OMS.Infrastructure.FileStorage;

public sealed class FileStorageOptions
{
    public string FtpHost { get; init; }
    public string FtpUsername { get; init; }
    public string FtpPassword { get; init; }
    public string DeliveryStoragePath { get; init; }
    public string ComplianceStoragePath { get; init; }
}
