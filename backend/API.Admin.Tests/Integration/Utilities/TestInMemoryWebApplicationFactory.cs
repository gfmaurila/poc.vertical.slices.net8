using API.Admin.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.Admin.Tests.Integration.Utilities;

public class TestInMemoryWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
        builder.UseEnvironment("Test");

        builder.ConfigureServices(services =>
        {
            // Remove the existing context configuration
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<EFSqlServerContext>));

            if (descriptor is not null)
                services.Remove(descriptor);

            // Add In-Memory context for testing
            services.AddDbContext<EFSqlServerContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            // Build the service provider
            var sp = services.BuildServiceProvider();

            // Create the schema in the In-Memory test database
            using var scope = sp.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<EFSqlServerContext>();
            appContext.Database.EnsureCreated();
        });
    }
}
