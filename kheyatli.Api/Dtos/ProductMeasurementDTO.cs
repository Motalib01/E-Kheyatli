namespace kheyatli.Api.Dtos;

public class ProductMeasurementDTO
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Guid PortfolioId { get; set; }
    public MeasurementType Type { get; set; }
    public string Value { get; set; }
    public string Notes { get; set; }
}