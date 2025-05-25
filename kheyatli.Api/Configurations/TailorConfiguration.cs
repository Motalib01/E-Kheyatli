using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kheyatli.Api.Configurations;

public class TailorConfiguration : IEntityTypeConfiguration<Tailor>
{
    public void Configure(EntityTypeBuilder<Tailor> builder)
    {
        builder.HasMany(t => t.Orders)
            .WithOne(o => o.Tailor)
            .HasForeignKey(o => o.TailorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.Chats)
            .WithOne(c => c.Tailor)
            .HasForeignKey(c => c.TailorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.MeasurementGuides)
            .WithOne(m => m.Tailor)
            .HasForeignKey(m => m.TailorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Portfolio)
            .WithOne(p => p.Tailor)
            .HasForeignKey<Tailor>(t => t.PortfolioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}