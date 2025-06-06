namespace kheyatli.Api.Dtos;

public class ProductDTO
{
    public Guid Id { get; set; }
    public Guid TailorId { get; set; }
    public string FabricPreferences { get; set; }
    public string StyleReferences { get; set; }
    public string Quote { get; set; }
    public string Notes { get; set; }
    public string ImageURL { get; set; }
}