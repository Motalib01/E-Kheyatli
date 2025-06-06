namespace kheyatli.Api.Dtos;

public class ClientDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Address { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}