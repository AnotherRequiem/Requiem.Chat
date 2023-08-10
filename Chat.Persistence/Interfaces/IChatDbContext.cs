using Chat.Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Interfaces;

public interface IChatDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Message> Messages { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}