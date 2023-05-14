namespace RecipesManagerWebClient.Web.Models;

public class Image
{
    public string Id { get; set; }

    public Guid OriginalPhotoGuid { get; set; }

    public Guid SmallPhotoGuid { get; set; }

    public string Extension { get; set; }

    // public ImageUploadStates ImageUploadState { get; set; }
}