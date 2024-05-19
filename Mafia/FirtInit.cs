using Microsoft.AspNetCore.Identity;
using Mafia.Domain.Entities;

namespace Mafia.Controllers
{
    public class FirtInit
    {
        public static async System.Threading.Tasks.Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@mail.ru";
            string adminSklad = "sklad@mail.ru";
            string password = "Test123!";

            if (await roleManager.FindByNameAsync("ADMIN") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("ADMIN"));
            }
            if (await roleManager.FindByNameAsync("DIRECTOR") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("DIRECTOR"));
            }
            if (await roleManager.FindByNameAsync("REGISTRATOR") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("REGISTRATOR"));
            }
           
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                ApplicationUser admin = new ApplicationUser { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "ADMIN");
                }
            }

            if (await userManager.FindByNameAsync(adminSklad) == null)
            {
                ApplicationUser admin = new ApplicationUser { Email = adminSklad, UserName = adminSklad, OrganisationId = 1 };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "REGISTRATOR");
                }
            }
        }
    }
}
