using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kheyatli.Api.Configurations;

public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.HasOne(p => p.Tailor)
            .WithOne(t => t.Portfolio)
            .HasForeignKey<Tailor>(t => t.PortfolioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}