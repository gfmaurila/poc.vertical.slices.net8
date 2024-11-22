namespace Common.Net8.Interface;

public interface IAuthService
{
    string GenerateJwtToken(string Id, string email, List<string> role);
    string GenerateJwtToken(string id, string email);
}
