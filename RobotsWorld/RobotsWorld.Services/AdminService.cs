using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RobotsWorld.Data;
using RobotsWorld.Models;
using RobotsWorld.Services.Constants;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.OutputModels.Users;

namespace RobotsWorld.Services
{
    public class AdminService : BaseService, IAdminService
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminService(UserManager<User> userManager, RobotsWorldContext context, IMapper mapper, RoleManager<IdentityRole> roleManager) : base(userManager, context, mapper)
        {
            this.roleManager = roleManager;
        }

        public async Task<IEnumerable<AdminUsersOutputModel>> GetAllUsers()
        {
            var users = this.Context.Users
                .Include(x => x.Robots)
                .Include(x => x.SentRobots)
                .Include(x => x.ReceivedRobots)
                .ToList();

            var modelUsers = Mapper.Map<IList<AdminUsersOutputModel>>(users);

            for (int i = 0; i < users.Count; i++)
            {
                var current = users[i];
                var role = await this.UserManager.GetRolesAsync(current);
                modelUsers[i].Role = role.FirstOrDefault() ?? GlobalConstants.DefaultRole;
            }

            return modelUsers;
        }

        public async Task DeleteUser(string userId)
        {
            var user = await this.UserManager.FindByIdAsync(userId);

            try
            {
                await DeleteUsersEntities(userId);
                await this.UserManager.DeleteAsync(user);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException(GlobalConstants.ErrorOnDeleteUser);
            }
        }

        private async Task DeleteUsersEntities(string userId)
        {
            var robots = this.Context.Robots
                .Include(x => x.Deliveries)
                .Include(x => x.Assembly)
                .ThenInclude(x => x.SubAssemblies)
                .ThenInclude(x => x.Parts)
                .Where(x => x.UserId == userId)
                .ToList();

            this.Context.Robots.RemoveRange(robots);

            await this.Context.SaveChangesAsync();
        }

        public async Task<IdentityResult> ChangeRole(ChangingRoleModel model)
        {
            string newRole = model.NewRole;
            var user = this.UserManager.FindByIdAsync(model.Id).Result;
            var currentRole = await this.UserManager.GetRolesAsync(user);
            IdentityResult result = null;

            result = await this.UserManager.RemoveFromRoleAsync(user, currentRole.First());
            result = await this.UserManager.AddToRoleAsync(user, newRole);

            return result;
        }

        public ChangingRoleModel AdminModifyRole(string Id)
        {
            var user = this.UserManager.FindByIdAsync(Id).Result;

            var model = this.Mapper.Map<ChangingRoleModel>(user);

            model.AppRoles = this.AppRoles();

            model.Role = this.UserManager.GetRolesAsync(user).Result.FirstOrDefault() ?? GlobalConstants.DefaultRole;

            return model;
        }

        private ICollection<string> AppRoles()
        {
            var result = this.roleManager.Roles.Select(x => x.Name).ToArray();

            return result;
        }
    }
}
