using API.Admin.Feature.Users.GetUser;
using API.Admin.Tests.Integration.Utilities;
using Common.Net8.Response;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace API.Admin.Tests.Integration.Features.Users;

public class GetUserTests : IClassFixture<DatabaseSQLServerFixture>
{
    private readonly HttpClient _client;
    private readonly DatabaseSQLServerFixture _fixture;

    public GetUserTests(DatabaseSQLServerFixture fixture)
    {
        _fixture = fixture;
        _client = fixture.Client;
    }

    [Fact]
    public async Task ShouldUser()
    {
        // Auth
        var token = await _fixture.GetAuthAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

        var httpResponse = await _client.GetAsync("/api/v1/user");
        httpResponse.EnsureSuccessStatusCode();
        var stringResponse = await httpResponse.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ApiResult<List<UserQueryModel>>>(stringResponse);
        _client.DefaultRequestHeaders.Clear();

        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        Assert.NotNull(result);
    }
}