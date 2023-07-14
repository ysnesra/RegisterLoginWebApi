namespace Core.Security.JWT;

public class AccessToken
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }  //tokenın süresi

    public string RefreshToken { get; set; }
}