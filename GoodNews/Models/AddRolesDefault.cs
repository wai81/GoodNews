using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodNews.DB;
using Microsoft.AspNetCore.Identity;
using Models;

namespace GoodNews.Models
{
    public class AddRolesDefault
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@ex.com";
            string password = "qwerty";

            string adminRoleName = "admin";
            string userRoleName = "user";

            if (await roleManager.FindByNameAsync(adminRoleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRoleName));
            }
            if (await roleManager.FindByNameAsync(userRoleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(userRoleName));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }

        }
    }
}
