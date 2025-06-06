using kheyatli.Api.Models;

public class Product : Base
{
    public Guid TailorId { get; set; }
    public Guid PortfolioId { get; set; }


    public string FabricPreferences { get; set; }
    public string StyleReferences { get; set; }
    public string Quote { get; set; }
    public string Notes { get; set; }
    

    public Tailor? Tailor { get; set; }

    public ICollection<ProductImage>? Images { get; set; }
    public ICollection<ProductMeasurement>? ProductMeasurement { get; set; }
    public Portfolio? Portfolio { get; set; }
}