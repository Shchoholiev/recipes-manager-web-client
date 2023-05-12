using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace RecipesManagerWebClient.Web.Configurations;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddGraphQlCLient(this IServiceCollection services, IConfiguration configuration)
	{
        services.AddScoped<GraphQLHttpClient>(p => 
            new GraphQLHttpClient(configuration.GetValue<string>("ApiUrl") + "graphql", new NewtonsoftJsonSerializer())
        );

		return services;
	}
}
