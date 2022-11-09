namespace SyncVR.Server.Models;

public record FibonacciQuery
{
    /// <summary>
    /// The unique query identifier.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The timestamp when the query was made.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// The Fibonacci number at the requested position.
    /// </summary>
    public ulong Value { get; init; }

    /// <summary>
    /// The requested position.
    /// </summary>
    public int Position { get; init; }

    /// <summary>
    /// An anonymous user identifier.
    /// </summary>
    public string ClientId { get; init; } = null!;
}
