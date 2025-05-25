using kheyatli.Api.Models;

public class ProductMeasurement : Base
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Guid PortfolioId { get; set; }

    public MeasurementType Type { get; set; }
    public string Value { get; set; } 
    public string Notes { get; set; }

    public Portfolio Portfolio { get; set; }
    public Order Order { get; set; }
    public Product Product { get; set; }

}