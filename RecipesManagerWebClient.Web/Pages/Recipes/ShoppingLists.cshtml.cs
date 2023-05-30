using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RecipesManagerWebClient.Web.Models;
using RecipesManagerWebClient.Web.Network;

namespace RecipesManagerWebClient.Web.Pages.Recipes
{
    public class ShoppingListsModel : PageBase
    {
        public List<ShoppingList> ShoppingLists { get; set; }

        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }


        private readonly ApiClient _apiClient;

        public ShoppingListsModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task OnGetAsync()
        {
            var page = Request.Query["page"];
            var pageNumber = 1;

            if (!string.IsNullOrEmpty(page) && int.TryParse(page, out int parsedPageNumber))
            {
                pageNumber = parsedPageNumber;
            }
            var request = new GraphQLRequest
            {
                Query = @"query Query($pageNumber: Int!, $pageSize: Int!) {
                      shoppingListsPage(pageNumber: $pageNumber, pageSize: $pageSize) {
                        items {
                          id
                          name
                          ingredients {
                            id
                            name
                            units
                            amount
                          }
                          recipes {
                            id
                            name
                            ingredientsText
                          }
                          sentTo {
                            id
                            email
                          }
                        }
                        hasNextPage
                        hasPreviousPage
                        pageNumber
                        pageSize
                        totalItems
                        totalPages
                      }
                    }",

                Variables = new
                {
                    pageNumber,
                    pageSize = 12
                }
            };

            var response = await _apiClient.QueryAsync(request);
            var jsonResponse = JsonConvert.SerializeObject(response.Data.shoppingListsPage.items);
            this.ShoppingLists = JsonConvert.DeserializeObject<List<ShoppingList>>(jsonResponse);

            TotalPages = response.Data.shoppingListsPage.totalPages;
            CurrentPage = pageNumber;
        }
    }
}
