using Chat.Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityTypeConfigurations;
using Persistence.Interfaces;

namespace Persistence;

public class ChatDbContext : DbContext, IChatDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }

    public ChatDbContext(DbContextOptions<ChatDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}