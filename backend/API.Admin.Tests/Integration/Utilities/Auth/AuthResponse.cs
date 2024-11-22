using Newtonsoft.Json;

namespace API.Admin.Tests.Integration.Utilities.Auth;

public class AuthResponse
{
    [JsonProperty("tokenType")]
    public string TokenType { get; set; }

    [JsonProperty("accessToken")]
    public string AccessToken { get; set; }

    [JsonProperty("refreshToken")]
    public string RefreshToken { get; set; }

    [JsonProperty("refreshTokenExpireAt")]
    public DateTime RefreshTokenExpireAt { get; set; }

    [JsonProperty("twoFactor")]
    public object TwoFactor { get; set; }
}
