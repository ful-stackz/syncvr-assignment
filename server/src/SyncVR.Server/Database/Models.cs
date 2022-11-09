namespace SyncVR.Server.Database;

public record FibonacciEntry
{
    /// <summary>
    /// Indicates the Nth position of this entry in the Fibonacci series.
    /// </summary>
    public int Position { get; init; }

    /// <summary>
    /// The Fibonacci value at this entry position.
    /// </summary>
    public ulong Value { get; init; }

    /// <summary>
    /// The queries associated with this entry.
    /// </summary>
    public List<FibonacciQuery> Queries { get; init; } = null!;
}

public record FibonacciQuery
{
    /// <summary>
    /// Auto-generated ID for this user query.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// An anonymous user identifier.
    /// </summary>
    public string ClientId { get; init; } = null!;

    /// <summary>
    /// The timestamp when the query was made.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// The Fibonacci position requested with this query.
    /// </summary>
    public int FibonacciPosition { get; init; }

    /// <summary>
    /// The Fibonacci entry associated with this query.
    /// </summary>
    public FibonacciEntry Entry { get; init; } = null!;
}
