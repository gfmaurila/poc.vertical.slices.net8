namespace Common.Net8.Helper;

public class PasswordResetInfo
{
    public string UserName { get; set; }
    public string ResetLink { get; set; }
    public TimeSpan ExpiryTime { get; set; }
}

