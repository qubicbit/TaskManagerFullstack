using Microsoft.EntityFrameworkCore;

namespace TaskManagerApi.Data.Dev
{
    public static class DevDatabaseReset
    {
        public static async Task ResetAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<AppDbContext>();

            // 1. Ta bort databasen helt
            await context.Database.EnsureDeletedAsync();

            // 2. Skapa databasen igen
            //await context.Database.EnsureCreatedAsync();
            await context.Database.MigrateAsync();

            // 3. Kör seeders i rätt ordning
            await IdentitySeeder.SeedAsync(services);
            await PublicTaskSeeder.SeedAsync(services);
            await DataSeeder.SeedUsersAndTasks(services);
        }
    }
}
