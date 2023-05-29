namespace RecipesManagerWebClient.Web.Models
{
    public class ShoppingList
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Recipe> Recipes { get; set; }
        public string Notes { get; set; }
        public List<User> SentTo { get; set; }
    }
}
