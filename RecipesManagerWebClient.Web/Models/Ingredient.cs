namespace RecipesManagerWebClient.Web.Models
{
    public class Ingredient
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Units { get; set; }
        public float Amount { get; set; }
        public int CaloriesPerUnit { get; set; }
        public int TotalCalories { get; set; }
    }
}
