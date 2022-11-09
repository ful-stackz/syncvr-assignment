using Microsoft.EntityFrameworkCore;
using SyncVR.Server.Database;

namespace SyncVR.Server.Stores;

public class FibonacciStore
{
    private readonly FibonacciDbContext _db;

    public FibonacciStore(FibonacciDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Checks if an entry for the given <paramref name="position"/> exists and
    /// if it does not - inserts a new entry with the given <paramref name="value"/>.
    /// </summary>
    public async Task<FibonacciEntry> AddEntryIfNotExistsAsync(int position, ulong value)
    {
        // If entry already exists don't insert anything
        var existing = await _db.Entries.FirstOrDefaultAsync(x => x.Position == position);
        if (existing is not null)
        {
            return existing;
        }

        // Entry does not exist, insert
        var inserted = await _db.Entries.AddAsync(new FibonacciEntry
        {
            Position = position,
            Value = value,
        });
        await _db.SaveChangesAsync();
        return inserted.Entity;
    }

    public async Task<FibonacciEntry?> GetEntryByPositionAsync(int position)
    {
        return await _db.Entries.FirstOrDefaultAsync(x => x.Position == position);
    }
}
