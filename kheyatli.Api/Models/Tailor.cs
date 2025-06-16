using kheyatli.Api.Models;

public class Tailor : Base
{
    public Guid UserId { get; set; }
    public Guid? PortfolioId { get; set; }

    public string? Brand { get; set; }
    public string? Bio { get; set; }
    public string? Address { get; set; }

    public bool? IsActive { get; set; } = true;
    public DateTime? CreatedAt { get; set; }
    public Category? Category { get; set; }

    public User? User { get; set; }
    public Portfolio? Portfolio { get; set; }

    public ICollection<Order>? Orders { get; set; }
    public ICollection<Chat>? Chats { get; set; }

    public ICollection<MeasurementsGuide> MeasurementGuides { get; set; }
}