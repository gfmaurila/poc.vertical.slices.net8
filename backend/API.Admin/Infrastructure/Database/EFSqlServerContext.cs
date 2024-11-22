using API.Admin.Domain.User;
using API.Admin.Infrastructure.Database.Mappings;
using Microsoft.EntityFrameworkCore;

namespace API.Admin.Infrastructure.Database;

public class EFSqlServerContext : DbContext
{
    public EFSqlServerContext()
    { }

    public EFSqlServerContext(DbContextOptions<EFSqlServerContext> options) : base(options)
    { }

    public virtual DbSet<UserEntity> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(_loggerFactory);
        base.OnConfiguring(optionsBuilder);
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        // Defina o provedor de banco de dados SQL Server ou outro aqui.
    //        optionsBuilder.UseSqlServer("Server=127.0.0.1;Integrated Security=true;Initial Catalog=core_test;User Id=sa;Password=@Poc2Minimal@Api;Trusted_Connection=false;MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=True;");
    //    }

    //    optionsBuilder.UseLoggerFactory(_loggerFactory);
    //    base.OnConfiguring(optionsBuilder);
    //}

    public static readonly LoggerFactory _loggerFactory = new LoggerFactory(new[] {
        new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
    });
}
