namespace kheyatli.Api.Dtos;

public class OrderDTO
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid TailorId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal Price { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? PaidAt { get; set; }
    public string DeliveryAddress { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
}