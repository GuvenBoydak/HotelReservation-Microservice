namespace IdentityService.Api.Models;

public class AccessToken
{
    public string Token { get; set; }

    public DateTime Expiration { get; set; }
}