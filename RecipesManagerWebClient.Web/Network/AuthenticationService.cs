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

        if (string.IsNullOrEmpty(jwtToken)) {
            jwtToken = await AccessWebGuest();
        }
        if (IsJwtTokenExpired(jwtToken))
        {
            jwtToken = await RefreshToken();
        }
        return jwtToken;
    }

    public bool IsJwtTokenExpired(string jwtToken)
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

    public string? GetEmailFromJwtToken(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadJwtToken(jwtToken);
        var claims = token.Claims;
        var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        return email;
    }

    public string? GetNameFromJwtToken(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadJwtToken(jwtToken);
        var claims = token.Claims;
        var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        return name;
    }

    public string? GetPhoneFromJwtToken(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadJwtToken(jwtToken);
        var claims = token.Claims;
        var phone = claims.FirstOrDefault(c => c.Type == ClaimTypes.MobilePhone)?.Value;

        return phone;
    }

    public string? GetIdFromJwtToken(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadJwtToken(jwtToken);
        var claims = token.Claims;
        var id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        return id;
    }

    private async Task<string> AccessWebGuest()
    {
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

        return tokens.AccessToken;
    }

    private async Task<string> RefreshToken()
    {
        var accessToken = _httpContext.Request.Cookies["accessToken"];
        var refreshToken = _httpContext.Request.Cookies["refreshToken"];

        var request = new GraphQLRequest
        {
            Query = @"
                    mutation RefreshUserToken($model: TokensModelInput!) {
                        refreshUserToken(model: $model) {
                            accessToken
                            refreshToken
                        }
                    }",
            Variables = new { model = new { accessToken = accessToken, refreshToken = refreshToken } }
        };

        var response = await _graphQLClient.SendMutationAsync<dynamic>(request);
        var jsonResponse = JsonConvert.SerializeObject(response.Data.refreshUserToken);
        var tokens = JsonConvert.DeserializeObject<TokensModel>(jsonResponse);
        _httpContext.Response.Cookies.Delete("accessToken");
        _httpContext.Response.Cookies.Delete("refreshToken");
        _httpContext.Response.Cookies.Append("accessToken", tokens.AccessToken, new CookieOptions { Expires = DateTime.UtcNow.AddDays(180) });
        _httpContext.Response.Cookies.Append("refreshToken", tokens.RefreshToken, new CookieOptions { Expires = DateTime.UtcNow.AddDays(180) });

        return tokens.AccessToken;
    }
}
