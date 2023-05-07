namespace AlasimVar.Application.Helpers.Jwt;

public class TokenOptions
{
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
    public int AccessTokenExpretion { get; set; }
    public string? Key { get; set; }
}