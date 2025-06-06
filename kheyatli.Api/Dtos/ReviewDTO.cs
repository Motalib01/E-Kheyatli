namespace kheyatli.Api.Dtos;

public class ReviewDTO
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ClientId { get; set; }
    public Guid TailorId { get; set; }
    public int Rate { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}