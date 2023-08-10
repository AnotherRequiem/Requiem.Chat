using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interfaces;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        services.AddDbContext<ChatDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
        services.AddScoped<IChatDbContext>(provider => provider.GetService<ChatDbContext>());
        return services;
    }
}