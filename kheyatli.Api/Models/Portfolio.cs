using kheyatli.Api.Models;

public class Portfolio : Base
{
    public string Name { get; set; }
    public string Description { get; set; }
    

    public ICollection<Product>? Products { get; set; }

    public Guid TailorId { get; set; }
    
    public Tailor? Tailor { get; set; }
}