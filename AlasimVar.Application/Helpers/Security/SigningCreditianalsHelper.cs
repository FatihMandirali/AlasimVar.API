using Microsoft.IdentityModel.Tokens;

namespace AlasimVar.Application.Helpers.Security;

public class SigningCreditianalsHelper
{
    public static SigningCredentials CreateSigningCreditianals(SecurityKey securityKey)
    {
        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
    }
}