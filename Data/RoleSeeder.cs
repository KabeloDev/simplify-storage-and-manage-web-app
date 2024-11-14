using Microsoft.AspNetCore.Identity;
using System.Data;

namespace TextEditor.Data
{
    public class RoleSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<IdentityUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            //Adding some roles to db 


            await roleManager.CreateAsync(new IdentityRole("User"));
            
        }
    }
}
