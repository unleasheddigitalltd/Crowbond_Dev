using Crowbond.Common.Domain;

namespace Crowbond.Common.Application.Exceptions;

public sealed class CrowbondException : Exception
{
    public CrowbondException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
