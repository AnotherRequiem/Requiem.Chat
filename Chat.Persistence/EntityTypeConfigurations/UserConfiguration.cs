using Chat.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(us => us.Id);
        builder.HasIndex(us => us.Id).IsUnique();
        builder.Property(us => us.Name).IsRequired().HasMaxLength(15);
    }
}