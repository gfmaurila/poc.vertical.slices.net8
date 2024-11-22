using API.Admin.Infrastructure.Database;
using API.Admin.Tests.Integration.Utilities.Auth;
using Microsoft.EntityFrameworkCore;

namespace API.Admin.Tests.Integration.Utilities;

public class DatabaseSQLServerFixture : IAsyncLifetime
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public HttpClient Client { get; }
    private readonly AuthToken1 _auth;
    private EFSqlServerContext _context;

    private static Random random = new Random();

    public DatabaseSQLServerFixture()
    {
        _auth = new AuthToken1();
        _factory = new TestWebApplicationFactory<Program>();
        Client = _factory.CreateClient();

        // Configurando o DbContext para usar In-Memory Database
        var options = new DbContextOptionsBuilder<EFSqlServerContext>()
            .UseInMemoryDatabase("InMemoryDbForTesting_" + random.Next())
            .Options;

        _context = new EFSqlServerContext(options);
    }

    public async Task InitializeAsync()
    {
        // Como estamos usando um banco In-Memory, EnsureCreated é suficiente.
        await _context.Database.EnsureCreatedAsync();
    }

    public TestWebApplicationFactory<Program> Factory()
    {
        return _factory;
    }

    public async Task DisposeAsync()
    {
        // Nenhuma ação necessária para excluir o banco de dados In-Memory
        await _context.DisposeAsync();
    }

    public async Task<AuthResponse> GetAuthAsync()
    {
        return new AuthResponse()
        {
            AccessToken = GenerateJwtToken()
        };
    }

    public string GenerateJwtToken()
    {
        return _auth.GenerateJwtToken();
    }
}
