using Abyat.Api.Constants;
using Abyat.Bl.Settings;
using Abyat.Da.Context;
using Abyat.Da.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Abyat.Api.Services.General;

public class ContextConfig
{
    public static async Task SeedDataAsync(
        AbyatDbContext context,
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        IOptions<AppSettings> emailOptions)
    {
        string seedAdminEmail = emailOptions.Value.Email.AdminEmail
            ?? throw new ArgumentNullException("Email:AdminEmail", "Admin email is missing in configuration.");

        string seedAdminPassword = emailOptions.Value.Email.AdminPassword
            ?? throw new ArgumentNullException("Email:AdminPassword", "Admin password is missing in configuration.");

        await SeedUserAsync(userManager, roleManager, seedAdminEmail, seedAdminPassword);
    }

    private static async Task SeedUserAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, string seedAdminEmail, string seedAdminPassword)
    {
        if (!await roleManager.RoleExistsAsync(SystemRoles.Admin))
            await roleManager.CreateAsync(new AppRole(SystemRoles.Admin));

        if (!await roleManager.RoleExistsAsync(SystemRoles.User))
            await roleManager.CreateAsync(new AppRole(SystemRoles.User));

        AppUser? adminUser = await userManager.FindByEmailAsync(seedAdminEmail);

        if (adminUser == null)
        {
            adminUser = new AppUser
            {
                UserName = seedAdminEmail,
                Email = seedAdminEmail,
                EmailConfirmed = true,
                FirstName = "admin",
                LastName = "admin",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = await userManager.CreateAsync(adminUser, seedAdminPassword);

            if (result.Succeeded)
                await userManager.AddToRoleAsync(adminUser, SystemRoles.Admin);
            else
                throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));
        }
    }
}
