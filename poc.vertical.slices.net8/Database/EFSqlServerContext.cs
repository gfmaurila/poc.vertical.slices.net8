using Microsoft.EntityFrameworkCore;
using poc.vertical.slices.net8.Domain;

namespace poc.vertical.slices.net8.Database;

public class EFSqlServerContext : DbContext
{
    public EFSqlServerContext()
    { }

    public EFSqlServerContext(DbContextOptions<EFSqlServerContext> options) : base(options)
    { }

    public virtual DbSet<Article> Article { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(builder => builder.OwnsOne(a => a.Tags, tagsBuilder => tagsBuilder.ToJson()));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(_loggerFactory);
        base.OnConfiguring(optionsBuilder);
    }

    public static readonly Microsoft.Extensions.Logging.LoggerFactory _loggerFactory = new LoggerFactory(new[] {
        new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
    });
}
