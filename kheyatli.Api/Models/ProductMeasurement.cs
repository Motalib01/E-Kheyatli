using kheyatli.Api.Models;

public class ProductMeasurement : Base
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Guid PortfolioId { get; set; }

    public float? Chest { get; set; }
    public float? Waist { get; set; }
    public float? Hip { get; set; }
    public float? SleeveLength { get; set; }
    public float? Inseam { get; set; }
    public float? Height { get; set; }
    public float? Notes { get; set; }

    public Portfolio? Portfolio { get; set; }
    public Order? Order { get; set; }
    public Product? Product { get; set; }

}