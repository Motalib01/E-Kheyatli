using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kheyatli.Api.Configurations;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasOne(a => a.User)
               .WithOne()
               .HasForeignKey<Admin>(a => a.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}