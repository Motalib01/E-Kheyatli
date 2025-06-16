using kheyatli.Api.Models;

public class Review : Base
{
    public Guid ClientId { get; set; }
    public Guid TailorId { get; set; }

    public int? Rate { get; set; } 
    public string? Comment { get; set; }
    public DateTime? CreatedAt { get; set; }

    public Client? Client { get; set; }
    public Tailor? Tailor { get; set; }
}