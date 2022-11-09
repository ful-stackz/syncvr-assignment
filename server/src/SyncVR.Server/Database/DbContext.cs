using Microsoft.EntityFrameworkCore;

namespace SyncVR.Server.Database;

public class FibonacciDbContext : DbContext
{
    private readonly IConfiguration _config;

    public FibonacciDbContext(IConfiguration config)
    {
        _config = config;
    }

    public DbSet<FibonacciQuery> Queries { get; set; } = null!;
    public DbSet<FibonacciEntry> Entries { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder
            .UseNpgsql(_config.GetValue<string>(ConfigKey.ConnectionString))
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        var entry = builder.Entity<FibonacciEntry>();
        entry.ToTable("fibonacci_series");
        entry.HasKey(x => x.Position);
        entry.HasMany(x => x.Queries)
            .WithOne(x => x.Entry)
            .HasPrincipalKey(x => x.Position)
            .HasForeignKey(x => x.FibonacciPosition);

        var query = builder.Entity<FibonacciQuery>();
        query.ToTable("queries");
        query.HasKey(x => x.Id);
        query.HasOne(x => x.Entry)
            .WithMany(x => x.Queries)
            .HasPrincipalKey(x => x.Position)
            .HasForeignKey(x => x.FibonacciPosition);
    }
}
