using GraphQL;
using Newtonsoft.Json;
using RecipesManagerWebClient.Web.Models;
using RecipesManagerWebClient.Web.Network;

namespace RecipesManagerWebClient.Web.Pages.Recipes
{
    public class SearchModel : PageBase
    {
        public List<Recipe> Recipes { get; set; }

         private readonly ApiClient _apiClient; 
 
        public SearchModel(ApiClient apiClient) 
        { 
            _apiClient = apiClient; 
        }

        public async Task OnGetAsync()
        {
            var search = Request.Query["search"];
            var type = Request.Query["type"];
            var request = new GraphQLRequest
            {
                Query = @"query SearchRecipes($recipeSearchType: RecipesSearchTypes!, $authorId: String!, $searchString: String!, $pageSize: Int!, $pageNumber: Int!, $categoriesIds: [String!]) {
                 searchRecipes(recipeSearchType: $recipeSearchType, authorId: $authorId, searchString: $searchString, pageSize: $pageSize, pageNumber: $pageNumber, categoriesIds: $categoriesIds) {
                 pageNumber
                 pageSize
                 totalItems
                 hasNextPage
                 hasPreviousPage
                 totalPages
                 items {
                 createdBy {
                 name
                 }
                 isSaved
                 name
                 thumbnail {
                 imageUploadState
                 id
                 smallPhotoGuid
                 extension
                 }
                 }
                 }
                 }",

                Variables = new
                {
                    recipeSearchType = type.FirstOrDefault() ?? "PUBLIC",
                    pageNumber = 1,
                    pageSize = 10,
                    categoriesIds = new string[0],
                    searchString = search.FirstOrDefault() ?? string.Empty,
                    authorId = ""
                }
            };

            var response = await _apiClient.QueryAsync(request);
            var jsonResponse = JsonConvert.SerializeObject(response.Data.searchRecipes.items);
            this.Recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonResponse);

        }

    }
}
