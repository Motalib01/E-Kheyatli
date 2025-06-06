namespace kheyatli.Api.Dtos;

public class MeasurementsGuideDTO
{
    public Guid Id { get; set; }
    public Guid TailorId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}