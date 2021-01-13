using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Data
{
    public static class SeedData
    {

        public static async Task Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
          await  SeedRoles(roleManager);
         await   SeedUsers(userManager);
        }

        private async static Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            if(await userManager.FindByEmailAsync("admin@admin.com")==null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com"
                };
               var result = await userManager.CreateAsync(user, "Test123.");
                if (result.Succeeded)
                {
                   await userManager.AddToRoleAsync(user, "Administrator");
                }
            }
            if (await userManager.FindByEmailAsync("user@user.com") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "user@user.com",
                    Email = "user@user.com"
                };
                var result = await userManager.CreateAsync(user, "Test123.");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }
        }
        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Employee"))
            {
                var role = new IdentityRole
                {
                    Name = "Employee"
                };
                await roleManager.CreateAsync(role);
            }
        }
    }
}
