using Microsoft.EntityFrameworkCore;
using SyncVR.Server.Database;

namespace SyncVR.Server.Stores;

public class QueriesStore
{
    private readonly FibonacciDbContext _db;

    public QueriesStore(FibonacciDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Retrieves all stored queries.
    /// </summary>
    public async Task<IEnumerable<Models.FibonacciQuery>> GetQueriesAsync()
    {
        return await _db.Queries
            .Include(x => x.Entry)
            .Select(x => new Models.FibonacciQuery
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                Position = x.Entry.Position,
                ClientId = x.ClientId,
                Value = x.Entry.Value,
            })
            .ToArrayAsync();
    }

    /// <summary>
    /// Stores the provided <paramref name="model"/> and returns an updated version
    /// with the assigned ID.
    /// </summary>
    public async Task<Models.FibonacciQuery> AddQueryAsync(Models.FibonacciQuery model)
    {
        var dbo = await _db.Queries.AddAsync(new FibonacciQuery
        {
            CreatedAt = DateTime.UtcNow,
            ClientId = model.ClientId,
            FibonacciPosition = model.Position,
        });
        await _db.SaveChangesAsync();

        return model with
        {
            Id = dbo.Entity.Id,
            CreatedAt = dbo.Entity.CreatedAt,
        };
    }
}
