using GraphQL;
using Newtonsoft.Json;
using RecipesManagerWebClient.Web.Models;
using RecipesManagerWebClient.Web.Network;

namespace RecipesManagerWebClient.Web.Pages.Menus
{
    public class MenusModel : PageBase
    {
        public List<Menu> Menus { get; set; }
        public List<Category> Categories { get; set; }
        public int TotalMenus { get; set; }

        private readonly ApiClient _apiClient;
        public MenusModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task OnGetAsync()
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
                Query = @"query MenusPage($pageNumber: Int!, $pageSize: Int!, $categoriesPageNumber2: Int!, $categoriesPageSize2: Int!) {
                  menusPage(pageNumber: $pageNumber, pageSize: $pageSize) {
                    hasNextPage
                    hasPreviousPage
                    items {
                      name
                      id
                    }
                    pageNumber
                    pageSize
                    totalItems
                    totalPages
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
                    pageNumber,
                    pageSize = 12,
                    categoriesPageNumber2 = 1,
                    categoriesPageSize2 = 10
                }
            };
            var response = await _apiClient.QueryAsync(request);
            var jsonResponse = JsonConvert.SerializeObject(response.Data.menusPage.items);
            this.Menus = JsonConvert.DeserializeObject<List<Menu>>(jsonResponse);

            var jsonCategoriesResponse = JsonConvert.SerializeObject(response.Data.categories.items);
            this.Categories = JsonConvert.DeserializeObject<List<Category>>(jsonCategoriesResponse);

            this.TotalMenus = response.Data.menusPage.totalItems;
        }
    }
}