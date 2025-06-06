namespace kheyatli.Api.Dtos;

public class ReportDTO
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid TailorId { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }
}