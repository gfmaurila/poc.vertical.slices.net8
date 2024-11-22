using API.Admin.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace poc.vertical.slices.net8.Extensions;

public static class WebApplicationExtensions
{
    public static async Task MigrateAsync(this WebApplication app)
    {
        await using var serviceScope = app.Services.CreateAsyncScope();
        await using var writeDbContext = serviceScope.ServiceProvider.GetRequiredService<EFSqlServerContext>();
        try
        {
            await app.MigrateDbContextAsync(writeDbContext);
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "Ocorreu uma exceção ao iniciar a aplicação: {Message}", ex.Message); throw;
        }
    }

    private static async Task MigrateDbContextAsync<TContext>(this WebApplication app, TContext context)
        where TContext : DbContext
    {
        var dbName = context.Database.GetDbConnection().Database;

        app.Logger.LogInformation("----- {DbName}: {DbConnection}", dbName, context.Database.GetConnectionString());
        app.Logger.LogInformation("----- {DbName}: Verificando se existem migrações pendentes...", dbName);

        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            app.Logger.LogInformation("----- {DbName}: Criando e migrando a base de dados...", dbName);

            await context.Database.MigrateAsync();

            app.Logger.LogInformation("----- {DbName}: Base de dados criada e migrada com sucesso!", dbName);
        }
        else
        {
            app.Logger.LogInformation("----- {DbName}: Migrações estão em dia.", dbName);
        }
    }
}
