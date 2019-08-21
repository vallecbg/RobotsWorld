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
using RobotsWorld.ViewModels.OutputModels.Robots;
using RobotsWorld.ViewModels.OutputModels.TransportTypes;
using RobotsWorld.ViewModels.OutputModels.Users;
using RobotsWorld.ViewModels.OutputModels.Vendors;

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
            ;
            try
            {
                var user = await this.UserManager.FindByIdAsync(userId);
                await DeleteUsersEntities(userId);
                await this.UserManager.DeleteAsync(user);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException(GlobalConstants.ErrorOnDeleteUser);
            }
        }

        public IEnumerable<VendorAdminOutputModel> GetAllVendors()
        {
            var vendors = this.Context.Vendors
                .Include(x => x.Parts)
                .ToList();

            var vendorsModel = this.Mapper.Map<IList<VendorAdminOutputModel>>(vendors);

            return vendorsModel;
        }

        public async Task<string> AddVendor(string vendorName)
        {
            var vendorExists = this.Context.Vendors.Any(x => x.Name == vendorName);

            if (!vendorExists)
            {
                var vendor = new Vendor{Name = vendorName};

                this.Context.Vendors.Add(vendor);
                await this.Context.SaveChangesAsync();

                return GlobalConstants.Success;
            }

            return GlobalConstants.Failed;
        }

        public async Task DeleteVendor(string vendorId, string username)
        {
            var vendor = this.Context.Vendors
                .Include(x => x.Parts)
                .FirstOrDefault(x => x.Id == vendorId);

            var user = await this.UserManager.FindByNameAsync(username);
            var roles = await this.UserManager.GetRolesAsync(user);

            bool hasRights = roles.Any(x => x == GlobalConstants.Admin);
            if (!hasRights)
            {
                throw new OperationCanceledException(GlobalConstants.UserHasNoRights);
            }

            this.Context.Vendors.Remove(vendor ?? throw new InvalidOperationException(GlobalConstants.RecordDoesntExist));
            await this.Context.SaveChangesAsync();
        }

        public IEnumerable<RobotAdminOutputModel> GetAllRobots()
        {
            var robots = this.Context.Robots
                .Include(x => x.Assembly)
                .ThenInclude(x => x.SubAssemblies)
                .ThenInclude(x => x.Parts)
                .Include(x => x.User)
                .Include(x => x.Deliveries)
                .ToList();

            var robotsModel = this.Mapper.Map<IList<RobotAdminOutputModel>>(robots);

            return robotsModel;
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

        public IEnumerable<TransportTypeOutputModel> GetAllTransportTypes()
        {
            var transportTypes = this.Context.TransportTypes
                .Include(x => x.Deliveries)
                .ToList();

            var transportTypesModel = this.Mapper.Map<IList<TransportTypeOutputModel>>(transportTypes);

            return transportTypesModel;
        }

        public async Task<string> AddTransportType(string transportTypeName)
        {
            var transportExists = this.Context.TransportTypes.Any(x => x.Name == transportTypeName);

            if (!transportExists)
            {
                var vendor = new TransportType { Name = transportTypeName };

                this.Context.TransportTypes.Add(vendor);
                await this.Context.SaveChangesAsync();

                return GlobalConstants.Success;
            }

            return GlobalConstants.Failed;
        }

        public async Task DeleteTransportType(string transportId, string username)
        {
            var transport = this.Context.TransportTypes
                .Include(x => x.Deliveries)
                .FirstOrDefault(x => x.Id == transportId);

            var user = await this.UserManager.FindByNameAsync(username);
            var roles = await this.UserManager.GetRolesAsync(user);

            bool hasRights = roles.Any(x => x == GlobalConstants.Admin);
            if (!hasRights)
            {
                throw new OperationCanceledException(GlobalConstants.UserHasNoRights);
            }

            this.Context.TransportTypes.Remove(transport ?? throw new InvalidOperationException(GlobalConstants.RecordDoesntExist));
            await this.Context.SaveChangesAsync();
        }

        public int GetUsersCount()
        {
            var usersCount = this.Context.Users.Count();

            return usersCount;
        }

        public int GetRobotsCount()
        {
            var robotsCount = this.Context.Robots.Count();

            return robotsCount;
        }

        public int GetVendorsCount()
        {
            var vendorsCount = this.Context.Vendors.Count();

            return vendorsCount;
        }

        public int GetTransportTypesCount()
        {
            var transportTypesCount = this.Context.TransportTypes.Count();

            return transportTypesCount;
        }

        public Dictionary<string, int> GetDeliveriesForAWeek()
        {
            DateTime[] last7Days = Enumerable.Range(0, 7)
                .Select(i => DateTime.Now.Date.AddDays(-i))
                .ToArray();


            var deliveries = this.Context.Deliveries
                .Where(x => last7Days.Any(c => c.Day == x.SentOn.Day && c.Month == x.SentOn.Month));
            var deliveriesReport = LoadCommentsReportWithDates();
            foreach (var delivery in deliveries)
            {
                var currentDate = delivery.SentOn.ToString("dddd");
                if (deliveriesReport.ContainsKey(currentDate))
                {
                    deliveriesReport[currentDate]++;
                }
            }

            return deliveriesReport;
        }

        public Dictionary<string, int> GetTop3RobotsWithMostDeliveries()
        {
            var robots = this.Context.Robots
                .Include(x => x.Deliveries)
                .Include(x => x.User)
                .OrderByDescending(x => x.Deliveries.Count)
                .Take(3)
                .ToList();
            
            var robotsModel = new Dictionary<string, int>();
            foreach (var robot in robots)
            {
                if (!robotsModel.ContainsKey(robot.Name))
                {
                    robotsModel.Add(robot.Name, 0);
                }

                robotsModel[robot.Name] += robot.Deliveries.Count;
            }

            return robotsModel;
        }

        private Dictionary<string, int> LoadCommentsReportWithDates()
        {
            var commentsReport = new Dictionary<string, int>();

            commentsReport.Add("Monday", 0);
            commentsReport.Add("Tuesday", 0);
            commentsReport.Add("Wednesday", 0);
            commentsReport.Add("Thursday", 0);
            commentsReport.Add("Friday", 0);
            commentsReport.Add("Saturday", 0);
            commentsReport.Add("Sunday", 0);

            return commentsReport;
        }
    }
}
