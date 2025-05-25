using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kheyatli.Api.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(u => u.Client)
            .WithOne(c => c.User)
            .HasForeignKey<Client>(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.Tailor)
            .WithOne(t => t.User)
            .HasForeignKey<Tailor>(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}