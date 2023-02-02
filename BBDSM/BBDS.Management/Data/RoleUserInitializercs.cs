using BBDS.Management.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
namespace BBDS.Management.Data
{
    public static class RoleUserInitializercs
    {

        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<RegisterViewModel>>();

            string[] roleNames = { "Admin", "Medic", "User" };

            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var email = "nikolov3007@gmail.com";
            var password = "Aa!123456";

            if (userManager.FindByEmailAsync(email).Result == null)
            {
                RegisterViewModel user = new()
                {
                    Password= password,
                    FirstName = "Deyan",
                    LastName = "Nikolov",
                    EGN = "7777777777",
                    BloodId = 7,
                    CityId = Guid.Parse("D0E60734-F384-4CCB-B472-61E8E9629E48"),
                    UserName = "TestUser1",
                    Email = email,
                    PhoneNumber = "7777777777"
                };

                IdentityResult result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            email = "medic@gmail.com";

            if (userManager.FindByEmailAsync(email).Result == null)
            {
                RegisterViewModel user = new()
                {
                    Password = password,
                    FirstName = "Medic",
                    LastName = "Medicov",
                    EGN = "8888888888",
                    BloodId = 1,
                    CityId = Guid.Parse("F40854CE-7D63-46A4-BB60-07DDE1A4705E"),
                    UserName = "Medic",
                    Email = email,
                    PhoneNumber = "8888888888"
                };

                IdentityResult result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Medic").Wait();
                }
            }

            email = "admin@gmail.com";

            if (userManager.FindByEmailAsync(email).Result == null)
            {
                RegisterViewModel user = new()
                {
                    Password = password,
                    FirstName = "Admin",
                    LastName = "Adminson",
                    EGN = "999999999",
                    BloodId = 3,
                    CityId = Guid.Parse("72316B2D-31A5-4BA8-8609-13719F7CCE98"),
                    UserName = "Admin",
                    Email = email,
                    PhoneNumber = "999999999"
                };

                IdentityResult result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}


