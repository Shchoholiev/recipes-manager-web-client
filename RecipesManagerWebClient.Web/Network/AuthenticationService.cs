using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GraphQL;
using GraphQL.Client.Http;
using Newtonsoft.Json;
using RecipesManagerWebClient.Web.Models.Identity;

namespace RecipesManagerWebClient.Web.Network;

public class AuthenticationService
{
    private readonly HttpContext _httpContext;
    
    private readonly GraphQLHttpClient _graphQLClient;

    public AuthenticationService(
        IHttpContextAccessor httpContextAccessor,
        GraphQLHttpClient graphQLClient)
    {
        _httpContext = httpContextAccessor.HttpContext;
        _graphQLClient = graphQLClient;
    }

    public async Task<string> GetAuthTokenAsync()
    {
        var jwtToken = _httpContext.Request.Cookies["accessToken"];

        if (string.IsNullOrEmpty(jwtToken) || IsJwtTokenExpired(jwtToken)) {
            var webId = _httpContext.Request.Cookies["webId"];
            if (string.IsNullOrEmpty(webId))
            {
                webId = Guid.NewGuid().ToString();
                _httpContext.Response.Cookies.Append("webId", webId, new CookieOptions { Expires = DateTime.UtcNow.AddDays(180) });
            }

            var request = new GraphQLRequest
            {
                Query = @"
                    mutation AccessWebGuest($user: AccessWebGuestModelInput!) {
                        accessWebGuest(model: $user) {
                            accessToken
                            refreshToken
                        }
                    }",
                Variables = new { user = new { webId = webId } } 
            };

            var response = await _graphQLClient.SendMutationAsync<dynamic>(request);
            var jsonResponse = JsonConvert.SerializeObject(response.Data.accessWebGuest);
            var tokens = JsonConvert.DeserializeObject<TokensModel>(jsonResponse);
            _httpContext.Response.Cookies.Append("accessToken", tokens.AccessToken, new CookieOptions { Expires = DateTime.UtcNow.AddDays(180) });
            _httpContext.Response.Cookies.Append("refreshToken", tokens.RefreshToken, new CookieOptions { Expires = DateTime.UtcNow.AddDays(180) });

            jwtToken = tokens.AccessToken;
        }

        return jwtToken;
    }

    private bool IsJwtTokenExpired(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.ReadJwtToken(jwtToken);
        var expiration = token.ValidTo;
        var isExpired = expiration < DateTime.UtcNow;

        return isExpired;
    }

    public List<string> GetRolesFromJwtToken(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadJwtToken(jwtToken);
        var claims = token.Claims;
        var roleClaims = claims.Where(c => c.Type == ClaimTypes.Role);
        var roles = roleClaims.Select(c => c.Value).ToList();

        return roles;
    }
}
