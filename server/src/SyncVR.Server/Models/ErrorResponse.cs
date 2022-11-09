namespace SyncVR.Server.Models;

public record ErrorResponse
{
    public string Error { get; init; } = string.Empty;
}
