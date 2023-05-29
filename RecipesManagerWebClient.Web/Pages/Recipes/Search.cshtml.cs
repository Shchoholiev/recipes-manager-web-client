using GraphQL;
using Newtonsoft.Json;
using RecipesManagerWebClient.Web.Models;
using RecipesManagerWebClient.Web.Network;

namespace RecipesManagerWebClient.Web.Pages.Recipes
{
    public class SearchModel : PageBase
    {
        public List<Recipe> Recipes { get; set; }
        public List<Category> Categories { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }


		private readonly ApiClient _apiClient;

        public SearchModel(ApiClient apiClient) 
        { 
            _apiClient = apiClient; 
        }

        public async Task OnGetAsync()
        {
            var search = Request.Query["search"];
            var type = Request.Query["type"];
            var page = Request.Query["page"];
			var pageNumber = 1; 

			if (!string.IsNullOrEmpty(page) && int.TryParse(page, out int parsedPageNumber))
			{
				pageNumber = parsedPageNumber;
			}
			var request = new GraphQLRequest
            {
                Query = @"query SearchRecipes($recipeSearchType: RecipesSearchTypes!, $categoriesIds: [String!], $pageNumber: Int!, $pageSize: Int!, $searchString: String!, $authorId: String!, $categoriesPageNumber2: Int!, $categoriesPageSize2: Int!) {
                  searchRecipes(recipeSearchType: $recipeSearchType, categoriesIds: $categoriesIds, pageNumber: $pageNumber, pageSize: $pageSize, searchString: $searchString, authorId: $authorId) {
                    hasNextPage
                    hasPreviousPage
                    pageNumber
                    pageSize
                    totalItems
                    totalPages
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
                    recipeSearchType = type.FirstOrDefault() ?? "PUBLIC",
                    pageNumber,
                    pageSize = 12,
                    categoriesIds = new string[0],
                    searchString = search.FirstOrDefault() ?? string.Empty,
                    authorId = "",
                    categoriesPageNumber2 = 1,
                    categoriesPageSize2 = 10
                }
            };

            var response = await _apiClient.QueryAsync(request);
            var jsonResponse = JsonConvert.SerializeObject(response.Data.searchRecipes.items);
            this.Recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonResponse);

            var jsonCategoriesResponse = JsonConvert.SerializeObject(response.Data.categories.items);
            this.Categories = JsonConvert.DeserializeObject<List<Category>>(jsonCategoriesResponse);

            TotalPages = response.Data.searchRecipes.totalPages;
            CurrentPage = pageNumber;
        }

    }

}

