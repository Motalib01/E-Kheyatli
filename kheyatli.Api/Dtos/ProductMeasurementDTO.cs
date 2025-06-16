namespace kheyatli.Api.Dtos;

public class ProductMeasurementDTO
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

}