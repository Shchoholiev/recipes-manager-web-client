using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StrawberryShake;

namespace RecipesManagerWebClient.Web.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddGraphQl(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient("GraphQlClient", client =>
        {
            client.BaseAddress = new Uri(configuration.GetValue<string>("ApiUrl") + "graphql/");
        });
        
		return services;
	}
}
