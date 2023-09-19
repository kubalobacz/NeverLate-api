using System.Security.Claims;

namespace NeverLate_api.Authentication.Token;

public interface IJwtDecoder
{
    IList<Claim>? DecodeToken(string jwt);
    IList<Claim>? DecodeToken(HttpRequest httpRequest);
}