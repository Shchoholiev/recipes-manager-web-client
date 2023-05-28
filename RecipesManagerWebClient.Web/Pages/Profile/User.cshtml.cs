using GraphQL;
using Newtonsoft.Json;
using RecipesManagerWebClient.Web.Models;
using RecipesManagerWebClient.Web.Models.GlobalInstances;
using RecipesManagerWebClient.Web.Network;

namespace RecipesManagerWebClient.Web.Pages.Profile
{
    public class UserModel : PageBase
    {
        public List<Recipe> Recipes { get; set; }
        public List<Category> Categories { get; set; }
        public User User { get; private set; }

        private readonly ApiClient _apiClient;

        public UserModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task OnGetAsync()
        {
            User = new User
            {
                Name = GlobalUser.Name,
                Id = GlobalUser.Id
            };
            var authorId = GlobalUser.Id;
            await LoadDataAsync(authorId);
        }

        private async Task LoadDataAsync(string authorId)
        {
            var search = Request.Query["search"];
            var request = new GraphQLRequest
            {
                Query = @"query SearchRecipes($recipeSearchType: RecipesSearchTypes!, $categoriesIds: [String!], $pageNumber: Int!, $pageSize: Int!, $searchString: String!, $authorId: String!, $categoriesPageNumber2: Int!, $categoriesPageSize2: Int!) {
                  searchRecipes(recipeSearchType: $recipeSearchType, categoriesIds: $categoriesIds, pageNumber: $pageNumber, pageSize: $pageSize, searchString: $searchString, authorId: $authorId) {
                    hasNextPage
                    hasPreviousPage
                    items {
                      categories {
                        id
                        name
                      }
                      createdBy {
                        id
                        name
                      }
                      id
                      isSaved
                      isPublic
                      minutesToCook
                      name
                      thumbnail {
                        smallPhotoGuid
                        imageUploadState
                        extension
                      }
                    }
                  }
                  categories(pageNumber: $categoriesPageNumber2, pageSize: $categoriesPageSize2) {
                    pageNumber
                    pageSize
                    items {
                      name
                    }
                  }
                }",
                Variables = new
                {
                    recipeSearchType = "PERSONAL",
                    pageNumber = 1,
                    pageSize = 12,
                    searchString = search.FirstOrDefault() ?? string.Empty,
                    authorId,
                    categoriesPageNumber2 = 1,
                    categoriesPageSize2 = 10
                }
            };

            var response = await _apiClient.QueryAsync(request);

            var jsonResponse = JsonConvert.SerializeObject(response.Data.searchRecipes.items);
            Recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonResponse);

            var jsonCategoriesResponse = JsonConvert.SerializeObject(response.Data.categories.items);
            Categories = JsonConvert.DeserializeObject<List<Category>>(jsonCategoriesResponse);
        }
    }
}
