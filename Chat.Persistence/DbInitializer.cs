namespace Persistence;

public class DbInitializer
{
    public static void Initialize(ChatDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
}