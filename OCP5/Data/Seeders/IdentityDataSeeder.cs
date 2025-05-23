using Microsoft.AspNetCore.Identity;

namespace OCP5.Data.Seeders;

public static class IdentityDataSeeder
{
    private const string AdminRole = "Admin";
    private const string AdminUser = "admin@express-voitures.fr";
    private const string AdminPassword = "P@ssword123";
    private const string NonAdminUser = "user1@testapp.fr";
    private const string NonAdminPassword = "P@ssword567";

    public static async Task EnsurePopulated(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetRequiredService(typeof(RoleManager<IdentityRole>));
        var role = await roleManager.FindByNameAsync(AdminRole);
        var isRoleAvailable = true;
        if (role == null)
        {
            role = new IdentityRole(AdminRole);
            var roleCreationResult = await roleManager.CreateAsync(role); 
            isRoleAvailable = roleCreationResult.Succeeded;
        }

        if (isRoleAvailable)
        {
            var userManager = (UserManager<IdentityUser>)scope.ServiceProvider.GetRequiredService(typeof(UserManager<IdentityUser>));
       
            await userManager.FindByEmailAsync(AdminUser);
            var adminUser = await userManager.FindByIdAsync(AdminUser);
            if (adminUser == null)
            {
                adminUser = new IdentityUser(AdminUser)
                {
                    Email = AdminUser,
                    UserName = AdminUser,
                    EmailConfirmed = true
                };
            
                var adminUserResult = await userManager.CreateAsync(adminUser, AdminPassword);
                if (adminUserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, AdminRole);
                }
            }
        
            var nonAdminUser = await userManager.FindByIdAsync(NonAdminUser);
            if (nonAdminUser == null)
            {
                nonAdminUser = new IdentityUser(NonAdminUser)
                {
                    Email = NonAdminUser,
                    UserName = NonAdminUser,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(nonAdminUser, NonAdminPassword);
            }
        }
    }
}