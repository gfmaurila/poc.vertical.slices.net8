using API.Admin.Infrastructure.Database;
using API.Admin.Tests.Integration.Utilities.Auth;
using Microsoft.EntityFrameworkCore;

namespace API.Admin.Tests.Integration.Utilities;

public class DatabaseSqliteFixture : IAsyncLifetime
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public HttpClient Client { get; }
    private readonly AuthToken1 _auth;
    private EFSqlServerContext _context;

    public DatabaseSqliteFixture()
    {
        _auth = new AuthToken1();
        _factory = new TestWebApplicationFactory<Program>();
        Client = _factory.CreateClient();

        // Configurando o DbContext para usar SQLite In-Memory
        var options = new DbContextOptionsBuilder<EFSqlServerContext>()
            .UseSqlite("DataSource=:memory:") // SQLite In-Memory
            .Options;

        _context = new EFSqlServerContext(options);
    }

    public async Task InitializeAsync()
    {
        // SQLite In-Memory precisa abrir a conexão para que o banco de dados exista
        await _context.Database.OpenConnectionAsync();
        await _context.Database.EnsureCreatedAsync();
    }

    public TestWebApplicationFactory<Program> Factory()
    {
        return _factory;
    }

    public async Task DisposeAsync()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.CloseConnectionAsync();
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
