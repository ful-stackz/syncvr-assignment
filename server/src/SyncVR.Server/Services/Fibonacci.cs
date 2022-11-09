using SyncVR.Server.Stores;

namespace SyncVR.Server.Services;

public class Fibonacci
{
    private readonly FibonacciStore _store;

    public Fibonacci(FibonacciStore store)
    {
        _store = store;
    }

    /// <summary>
    /// Calculates the Fibonacci number at the specified <paramref name="position"/>.
    /// </summary>
    public async Task<ulong> CalculateAsync(int position)
    {
        if (position == 0) return 0;

        // Try to find in database before calculating
        var found = await _store.GetEntryByPositionAsync(position);
        if (found is not null)
        {
            return found.Value;
        }

        // The Fibonacci formula is F(n) = F(n-1) + F(n-2)
        // Since we hande F(0), we can initialize the calculation variables for F(1)
        ulong n2 = 0;           // n-2 for F(1)
        ulong n1 = 1;           // n-1 for F(1)
        ulong fib = n1 + n2;    // F(1)

        // Loop until we reach the desired Nth position.
        // We calculate F(1) when initializing the variables,
        // therefore we can start the loop from F(2) and skip a calculation.
        for (int i = 1; i < position; i += 1)
        {
            fib = n1 + n2;
            n2 = n1;
            n1 = fib;
        }

        // Store in database
        await _store.AddEntryIfNotExistsAsync(position, fib);

        return fib;

        // quick reference
        // n    | 0 | 1 | 2 | 3 | 4 | 5 | 6 |
        // n-1  | - | - | 1 | 1 | 2 | 3 | 5 |
        // n-2  | - | - | 0 | 1 | 1 | 2 | 3 |
        // fib  | 0 | 1 | 1 | 2 | 3 | 5 | 8 |
    }
}
