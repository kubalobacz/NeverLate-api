using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;

namespace NeverLate_api.Authentication.Token;

public class JwtDecoder : IJwtDecoder
{
    public IList<Claim>? DecodeToken(string jwt)
    {
        var jwtSecurityHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = jwtSecurityHandler.ReadJwtToken(jwt);
        return jwtSecurityToken.Claims.ToList();
    }

    public IList<Claim>? DecodeToken(HttpRequest httpRequest)
    {
        var jwt = httpRequest.Headers[HeaderNames.Authorization].ToString().Replace($"{JwtBearerDefaults.AuthenticationScheme} ", string.Empty);
        return DecodeToken(jwt);
    }
}