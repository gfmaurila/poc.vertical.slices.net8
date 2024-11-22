using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Admin.Tests.Integration.Utilities.Auth;

public class AuthToken1 : IClassFixture<DatabaseSQLServerFixture>
{
    public string GenerateJwtToken()
    {
        SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("cHJqc2xuYmFjay1ndWlsaGVybWVtYXVyaWxh")), "HS256");
        List<Claim> list = new List<Claim>
        {
            new Claim("userName", "auth@auth.com.br"),
            new Claim("id", "9A749D84-5734-4FAA-95C2-CF2B209EBE89")
        };

        DateTime? expires = DateTime.Now.AddHours(8.0);
        SigningCredentials signingCredentials2 = signingCredentials;
        JwtSecurityToken token = new JwtSecurityToken("JwtApiAuth", "JwtApiAuth", list, null, expires, signingCredentials2);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}