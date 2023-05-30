namespace RecipesManagerWebClient.Web.Models
{
    public class Menu
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Recipe> Recipes { get; set; }
        public string? Notes { get; set; }
       // public List<ContactDto>? SentTo { get; set; }
        public DateTime? ForDateUtc { get; set; }

    }
}
