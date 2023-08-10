using Chat.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(ms => ms.Id);
        builder.HasIndex(ms => ms.Id).IsUnique();
        builder.Property(ms => ms.TimeStamp).IsRequired();
        builder.Property(ms => ms.Content).IsRequired().HasMaxLength(250);
    }
}