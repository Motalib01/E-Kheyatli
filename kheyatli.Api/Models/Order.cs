using kheyatli.Api.Models;

public class Order : Base
{
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

    public Client Client { get; set; }
    public Tailor Tailor { get; set; }
    public ICollection<ProductMeasurement> ProductMeasurements { get; set; }
}