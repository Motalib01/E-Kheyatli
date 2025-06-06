namespace kheyatli.Api.Models;

public class Report: Base
{
    public Guid ClientId { get; set; }
    public Guid TailorId { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }

    public Client? Client { get; set; }
    public Tailor? Tailor { get; set; }
}