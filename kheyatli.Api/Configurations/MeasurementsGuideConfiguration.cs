using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kheyatli.Api.Configurations;

public class MeasurementsGuideConfiguration : IEntityTypeConfiguration<MeasurementsGuide>
{
    public void Configure(EntityTypeBuilder<MeasurementsGuide> builder)
    {
        builder.HasOne(m => m.Tailor)
            .WithMany(t => t.MeasurementGuides)
            .HasForeignKey(m => m.TailorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}