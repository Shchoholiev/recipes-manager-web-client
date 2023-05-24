namespace RecipesManagerWebClient.Web.Models
{
    public class User
    {
        public string Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public DateTime? RefreshTokenExpiryDate { get; set; }

        public Guid? WebId { get; set; }

       // public List<RoleDto> Roles { get; set; }
    }
}
