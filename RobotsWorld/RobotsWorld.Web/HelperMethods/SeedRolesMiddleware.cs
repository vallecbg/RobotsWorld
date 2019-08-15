using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RobotsWorld.Models;
using RobotsWorld.Services.Constants;

namespace RobotsWorld.Web.HelperMethods
{
    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate _next;

        public SeedRolesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,
            UserManager<User> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await SeedRoles(userManager, roleManager);
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }

        private async Task SeedRoles(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole
            {
                Name = GlobalConstants.AdminRole
            });

            await roleManager.CreateAsync(new IdentityRole
            {
                Name = GlobalConstants.DefaultRole
            });

            var user = new User
            {
                UserName = "admintest",
                Email = "admin@admin.com",
                Name = "The Admin"
            };

            var normalUser = new User
            {
                UserName = "usertest",
                Email = "user@user.com",
                Name = "The User"
            };

            string normalUserPass = "user123";
            string adminPass = "admin123";

            await userManager.CreateAsync(user, adminPass);
            await userManager.CreateAsync(normalUser, normalUserPass);

            await userManager.AddToRoleAsync(user, GlobalConstants.AdminRole);
            await userManager.AddToRoleAsync(normalUser, GlobalConstants.DefaultRole);
        }
    }
}
