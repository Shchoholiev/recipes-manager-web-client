using GraphQL;
using GraphQL.Client.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RecipesManagerWebClient.Web.Models;

namespace RecipesManagerWebClient.Web.Pages.Recipes
{
    public class SearchModel : PageModel
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
                query RecipeSearchResult($recipeSearchType: RecipesSearchTypes!, $pageNumber: Int!, $pageSize: Int!, $categoriesIds: [String!], $searchString: String!, $authorId: String!) {
                    searchRecipes(recipeSearchType: $recipeSearchType, pageNumber: $pageNumber, pageSize: $pageSize, categoriesIds: $categoriesIds, searchString: $searchString, authorId: $authorId) {
                        items {
                        name
                        createdById
                        categories {
                            id
                        }
                        ingredientsText
                        ingredients {
                            name
                            units
                        }
                        createdBy {
                            name
                            id
                        }
                        },
                        totalItems
                    }
                }", 
                Variables = new 
                { 
                    recipeSearchType = "PUBLIC",
                    pageNumber = 1,
                    pageSize = 10,
                    categoriesIds = new string[0],
                    searchString = "",
                    authorId = ""
                }  
            }; 
    
            var response = await _graphQLClient.SendMutationAsync<dynamic>(request); 
            var jsonResponse = JsonConvert.SerializeObject(response.Data.searchRecipes.items);
            this.Recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonResponse);
        } 

    }
}
