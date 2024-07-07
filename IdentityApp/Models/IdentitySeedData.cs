using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Models;

public static class IdentitySeedData
{
    const string adminUser = "Admin";
    const string adminPassword = "Admin_123";

    public static async void IdentityTestUser(IApplicationBuilder app)
    {
        var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IdentityContext>();
        if (context.Database.GetAppliedMigrations().Any())
        {
            context.Database.Migrate();
        }

        var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var user = await userManager.FindByNameAsync(adminUser);
        if (user == null)
        {
            user = new IdentityUser
            {
                UserName = adminUser,
                Email = "admin@gmail.com",
                PhoneNumber = "111111111",
            };
            await userManager.CreateAsync(user, adminPassword);
        }
    }
}