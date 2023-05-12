using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using GraphQL;
using GraphQL.Client.Http;
using Newtonsoft.Json;
using RecipesManagerWebClient.Web.Models.Identity;

namespace RecipesManagerWebClient.Web.Configurations;

public class JwtMiddleware : IMiddleware
{
    private readonly GraphQLHttpClient _graphQLClient;

    public JwtMiddleware(GraphQLHttpClient graphQLClient)
    {
        _graphQLClient = graphQLClient;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var jwtToken = context.Request.Cookies["accessToken"];
        var webId = context.Request.Cookies["webId"];
        var count = context.Request.Cookies.Count;

        if (string.IsNullOrEmpty(jwtToken) || IsJwtTokenExpired(jwtToken))
        {
            if (string.IsNullOrEmpty(webId))
            {
                webId = Guid.NewGuid().ToString();
                context.Response.Cookies.Append("webId", webId, new CookieOptions { Expires = DateTime.UtcNow.AddDays(180) });
            }
            
            if (count > 0) {
                var tokens = await GetTokensAsync(webId);

                jwtToken = tokens.AccessToken;
                context.Response.Cookies.Append("accessToken", tokens.AccessToken, new CookieOptions { Expires = DateTime.UtcNow.AddDays(180) });
                context.Response.Cookies.Append("refreshToken", tokens.RefreshToken, new CookieOptions { Expires = DateTime.UtcNow.AddDays(180) });
            }
        }

        _graphQLClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

        await next(context);
    }

    private bool IsJwtTokenExpired(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.ReadJwtToken(jwtToken);
        var expiration = token.ValidTo;
        var isExpired = expiration < DateTime.UtcNow;

        return isExpired;
    }

    private async Task<TokensModel> GetTokensAsync(string webId)
    {
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

            GetRolesFromJwtToken(tokens.AccessToken);

        return tokens;
    }

    private List<string> GetRolesFromJwtToken(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadJwtToken(jwtToken);
        var claims = token.Claims;
        var roleClaims = claims.Where(c => c.Type == ClaimTypes.Role);
        var roles = roleClaims.Select(c => c.Value).ToList();

        return roles;
    }
}
