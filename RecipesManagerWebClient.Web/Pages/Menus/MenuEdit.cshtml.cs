using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RecipesManagerWebClient.Web.Models;
using RecipesManagerWebClient.Web.Network;

namespace RecipesManagerWebClient.Web.Pages.Menus
{
    public class MenuEditModel : PageBase
    {

        public Menu Menu { get; set; }
        public List<Category> Categories { get; set; }
        public List<Recipe> Recipes { get; set; }

        private readonly ApiClient _apiClient;
        public MenuEditModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task OnGetAsync(string menuId)
        {
            var search = Request.Query["search"];
            var page = Request.Query["page"];
            var pageNumber = 1;

            if (!string.IsNullOrEmpty(page) && int.TryParse(page, out int parsedPageNumber))
            {
                pageNumber = parsedPageNumber;
            }
            var request = new GraphQLRequest
            {
                Query = @"query Menu($menuId: String!, $pageNumber: Int!, $pageSize: Int!) {
                  menu(id: $menuId) {
                    id
                    name
                    notes
                    recipes {
                      createdById
                      id
                      isSaved
                      minutesToCook
                      name
                      thumbnail {
                        extension
                        imageUploadState
                        smallPhotoGuid
                      }
                    }
                  }
                  categories(pageNumber: $pageNumber, pageSize: $pageSize) {
                    pageNumber
                    pageSize
                    items {
                      name
                    }
                  }
                }",

                Variables = new
                {
                    menuId,
                    pageNumber,
                    pageSize = 10,
                }
            };
            var response = await _apiClient.QueryAsync(request);
            var jsonResponse = JsonConvert.SerializeObject(response.Data.menu);
            this.Menu = JsonConvert.DeserializeObject<Menu>(jsonResponse);

            var jsonCategoriesResponse = JsonConvert.SerializeObject(response.Data.categories.items);
            this.Categories = JsonConvert.DeserializeObject<List<Category>>(jsonCategoriesResponse);

            var jsonRecipesResponse = JsonConvert.SerializeObject(response.Data.menu.recipes);
            this.Recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonRecipesResponse);
        }
    }
}
