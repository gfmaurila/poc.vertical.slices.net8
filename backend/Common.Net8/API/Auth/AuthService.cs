using Common.Net8.AppSettings;
using Common.Net8.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Common.Net8.API.Auth;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(string id, string email, List<string> role)
    {
        var issuer = _configuration.GetValue<string>(AuthConsts.Issuer);
        var audience = _configuration.GetValue<string>(AuthConsts.Audience);
        var key = _configuration.GetValue<string>(AuthConsts.Key);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim("userName",email),
            new Claim("id",id),
        };
        foreach (var userRole in role)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole.ToString()));
        }
        var token = new JwtSecurityToken(issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: credentials,
            claims: claims);
        var tokenHandler = new JwtSecurityTokenHandler();
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }

    public string GenerateJwtToken(string id, string email)
    {
        var issuer = _configuration.GetValue<string>(AuthConsts.Issuer);
        var audience = _configuration.GetValue<string>(AuthConsts.Audience);
        var key = _configuration.GetValue<string>(AuthConsts.Key);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim("userName",email),
            new Claim("id",id),
            //new Claim(ClaimTypes.Role, ERoleUserAuth.AUTH_RESET.ToString())
        };
        var token = new JwtSecurityToken(issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials,
            claims: claims);
        var tokenHandler = new JwtSecurityTokenHandler();
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }

}
