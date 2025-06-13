using kheyatli.Api.Models;

public class Portfolio : Base
{
    public ICollection<Product>? Products { get; set; }

    public Guid? TailorId { get; set; }
    
    public Tailor? Tailor { get; set; }
}