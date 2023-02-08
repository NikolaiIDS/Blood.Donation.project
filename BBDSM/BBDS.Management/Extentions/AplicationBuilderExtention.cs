using BBDS.Management.Data;
using Microsoft.AspNetCore.Identity;

namespace BBDS.Management.Extentions
{
    public static class applicationbuilderextension
    {
        public static IApplicationBuilder SeedAdmin(this IApplicationBuilder app)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider services = scopedServices.ServiceProvider;

            UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    IdentityRole adminRole = new IdentityRole("Admin");
                    await roleManager.CreateAsync(adminRole);
                }

                if (!await roleManager.RoleExistsAsync("Medic"))
                {
                    IdentityRole adminRole = new IdentityRole("Medic");
                    await roleManager.CreateAsync(adminRole);
                }

                if (!await roleManager.RoleExistsAsync("User"))
                {
                    IdentityRole userRole = new IdentityRole("User");
                    await roleManager.CreateAsync(userRole);
                }

                ApplicationUser admin = await userManager.FindByNameAsync("admin");
                await userManager.AddToRoleAsync(admin, "Admin");

                ApplicationUser medic = await userManager.FindByNameAsync("medic");
                await userManager.AddToRoleAsync(medic, "Medic");

                ApplicationUser testingUser = await userManager.FindByNameAsync("user1");
                await userManager.AddToRoleAsync(testingUser, "User");
            })
            .GetAwaiter()
            .GetResult();

            return app;
        }
    }
}