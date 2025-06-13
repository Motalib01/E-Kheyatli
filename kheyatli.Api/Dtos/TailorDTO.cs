namespace kheyatli.Api.Dtos;

public class TailorDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid? PortfolioId { get; set; }
    public string Brand { get; set; }
    public string Bio { get; set; }
    public string Address { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public CategoryDTO Gategory { get; set; }
}