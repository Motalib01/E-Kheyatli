using kheyatli.Api.Models;

public class Category: Base
{
    public Guid TailorId { get; set; }
    public string Name { get; set; }
    public Tailor Tailor { get; set; }
}