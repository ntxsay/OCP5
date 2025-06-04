using Microsoft.AspNetCore.Identity;

namespace OCP5.Data.Seeders;

/// <summary>
/// Classe de peuplement des données d'identité dans la base de données.
/// </summary>
public static class IdentityDataSeeder
{
    public static async Task EnsurePopulated(IApplicationBuilder app, IConfiguration config)
    {
        var adminRole = config.GetSection("UserIds:AdminRole").Value;
        var adminUserName = config.GetSection("UserIds:AdminUser").Value;
        var adminPassword = config.GetSection("UserIds:AdminPassword").Value;
        var nonAdminUserName = config.GetSection("UserIds:NonAdminUser").Value;
        var nonAdminPassword = config.GetSection("UserIds:NonAdminPassword").Value;
        
        if (string.IsNullOrEmpty(adminUserName) || string.IsNullOrWhiteSpace(adminUserName) ||
            string.IsNullOrEmpty(adminPassword) || string.IsNullOrWhiteSpace(adminPassword) ||
            string.IsNullOrEmpty(nonAdminUserName) || string.IsNullOrWhiteSpace(nonAdminUserName) ||
            string.IsNullOrEmpty(nonAdminPassword) || string.IsNullOrWhiteSpace(nonAdminPassword) ||
            string.IsNullOrEmpty(adminRole) || string.IsNullOrWhiteSpace(adminRole))
        {
            return;
        }
        
        using var scope = app.ApplicationServices.CreateScope();
        var roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetRequiredService(typeof(RoleManager<IdentityRole>));
        var role = await roleManager.FindByNameAsync(adminRole);
        var isRoleAvailable = true;
        if (role == null)
        {
            role = new IdentityRole(adminRole);
            var roleCreationResult = await roleManager.CreateAsync(role); 
            isRoleAvailable = roleCreationResult.Succeeded;
        }

        if (isRoleAvailable)
        {
            var userManager = (UserManager<IdentityUser>)scope.ServiceProvider.GetRequiredService(typeof(UserManager<IdentityUser>));
       
            var adminUser = await userManager.FindByIdAsync(adminUserName);
            if (adminUser == null)
            {
                adminUser = new IdentityUser(adminUserName)
                {
                    Email = adminUserName,
                    UserName = adminUserName,
                    EmailConfirmed = true
                };
            
                var adminUserResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (adminUserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }
        
            var nonAdminUser = await userManager.FindByIdAsync(nonAdminUserName);
            if (nonAdminUser == null)
            {
                nonAdminUser = new IdentityUser(nonAdminUserName)
                {
                    Email = nonAdminUserName,
                    UserName = nonAdminUserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(nonAdminUser, nonAdminPassword);
            }
        }
    }
}