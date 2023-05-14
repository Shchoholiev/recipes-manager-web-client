using GraphQL;
using GraphQL.Client.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RecipesManagerWebClient.Web.Models;

namespace RecipesManagerWebClient.Web.Pages.Recipes
{
    public class SearchModel : PageBase
    {
        public List<Recipe> Recipes { get; set; }

         private readonly GraphQLHttpClient _graphQLClient; 
 
        public SearchModel(GraphQLHttpClient graphQLClient) 
        { 
            _graphQLClient = graphQLClient; 
        } 
    
        public async Task OnGetAsync() 
        { 
            var request = new GraphQLRequest 
            { 
                Query = @"
                query Recipe($recipeId: String!) {
                recipe(id: $recipeId) {
                    id
                    name
                    ingredients {
                    name
                    }
                    thumbnail {
                        imageUploadState
                        id
                        smallPhotoGuid
                        extension
                    }
                    ingredientsText
                    categories {
                    name
                    id
                    }
                    calories
                    servingsCount
                    isSaved
                    createdById
                    createdDateUtc
                }
                }", 
                Variables = new 
                {
                    recipeId = "645d32a6537ef8eec90db9f4"
                }
            }; 
    
            var response = await _graphQLClient.SendMutationAsync<dynamic>(request); 
            var jsonResponse = JsonConvert.SerializeObject(response.Data.recipe);
            this.Recipes = new List<Recipe> { JsonConvert.DeserializeObject<Recipe>(jsonResponse) };
        } 

    }
}
