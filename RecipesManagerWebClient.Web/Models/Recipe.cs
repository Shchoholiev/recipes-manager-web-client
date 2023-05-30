namespace RecipesManagerWebClient.Web.Models;

public class Recipe
{
    public string Id { get; set; }

    public string Name { get; set; }

    public Image? Thumbnail { get; set; }

    public List<Ingredient>? Ingredients { get; set; }

    public string? Text { get; set; }

    public string? IngredientsText { get; set; }

    public List<Category> Categories { get; set; }

    public int? Calories { get; set; }
    public int? MinutesToCook { get; set; }

    public int? ServingsCount { get; set; }

    public bool IsSaved { get; set; }
    public bool IsPublic { get; set; }

    public string CreatedById { get; set; }

    public User CreatedBy { get; set; }

    public DateTime CreatedDateUtc { get; set; }
}
