using kheyatli.Api.Models;

public class MeasurementsGuide : Base
{
    public Guid TailorId { get; set; }  // Foreign key to Tailor

    public string? Title { get; set; }
    public string? Content { get; set; }

    public Tailor? Tailor { get; set; }
}