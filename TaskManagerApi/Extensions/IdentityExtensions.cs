using Microsoft.AspNetCore.Identity;
using TaskManagerApi.Models;
using TaskManagerApi.Data;

namespace TaskManagerApi.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireUppercase = true;
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequiredLength = 8;

                //options.User.RequireUniqueEmail = true;

                // Password rules (dev mode)
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 1 ;
                options.Password.RequiredUniqueChars = 0;

                // Email rules (dev mode)
                options.User.RequireUniqueEmail = false;
                options.User.AllowedUserNameCharacters = null; // tillåter ALLA tecken


            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
