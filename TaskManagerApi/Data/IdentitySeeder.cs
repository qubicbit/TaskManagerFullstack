using Microsoft.AspNetCore.Identity;
using TaskManagerApi.Models;

// IdentitySeeder körs endast vid uppstart. Endast för initial setup.
// Den skapar rollerna "Admin" och "User" samt admin-användaren.
// Vanliga användare skapas via API och tilldelas rollen "User" i AuthService.

namespace TaskManagerApi.Data;

public static class IdentitySeeder
{
    public static async Task SeedAdmin(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var config = services.GetRequiredService<IConfiguration>();

        //  Skapa Admin-roll om den inte finns
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            Console.WriteLine("Admin role created");
        }

        //   Skapa User-roll om den inte finns
        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
            Console.WriteLine("User role created");
        }

        // Hämta admin-email från appsettings
        var adminEmail = config["AdminEmail"];
        var admin = await userManager.FindByEmailAsync(adminEmail);

        //  Skapa admin-användare om den inte finns
        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "System Administrator"
            };

            var adminPassword = config["AdminPassword"];
            var result = await userManager.CreateAsync(admin, adminPassword);

            if (!result.Succeeded)
            {
                Console.WriteLine("Admin creation failed:");
                foreach (var error in result.Errors)
                    Console.WriteLine(" - " + error.Description);
                return;
            }

            Console.WriteLine("Admin user created");
        }

        //  Tilldela Admin-roll om den saknas
        if (!await userManager.IsInRoleAsync(admin, "Admin"))
        {
            await userManager.AddToRoleAsync(admin, "Admin");
            Console.WriteLine("Admin role assigned");
        }

    }
    public static async Task SeedAsync(IServiceProvider services)
    {
        await SeedAdmin(services);
    }


}

