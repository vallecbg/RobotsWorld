using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RobotsWorld.Data;
using RobotsWorld.Models;
using RobotsWorld.Services.Constants;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.Robots;
using RobotsWorld.ViewModels.OutputModels.Robots;

namespace RobotsWorld.Services
{
    public class RobotService : BaseService, IRobotService
    {
        public RobotService(UserManager<User> userManager, RobotsWorldContext context, IMapper mapper) : base(userManager, context, mapper)
        {
        }

        public async Task<string> CreateRobot(RobotInputModel model)
        {
            var cloudinary = SetCloudinary();

            var url = await UploadImage(cloudinary, model.Image, model.Name);

            var robot = Mapper.Map<Robot>(model);

            robot.User = await this.UserManager.FindByNameAsync(model.User);
            robot.ImageUrl = url ?? GlobalConstants.NoImageAvailableUrl;

            this.Context.Robots.Add(robot);
            await this.Context.SaveChangesAsync();

            return robot.Id;
        }

        public ICollection<RobotOutputModel> GetUserRobots(string userId)
        {
            var robots = this.Context.Robots
                .Where(x => x.UserId == userId)
                .ToList();

            var robotsOutput = Mapper.Map<IList<RobotOutputModel>>(robots);

            return robotsOutput;
        }

        public RobotOutputModel GetRobotDetails(string robotId)
        {
            var robot = this.Context.Robots
                .Include(x => x.User)
                .Include(x => x.Assembly)
                .ThenInclude(x => x.SubAssemblies)
                .ThenInclude(x => x.Parts)
                .FirstOrDefault(x => x.Id == robotId);

            var robotOutput = Mapper.Map<RobotOutputModel>(robot);

            return robotOutput;
        }

        public void DeleteRobot(string robotId, string username)
        {
            var robot = this.Context.Robots
                .Include(x => x.User)
                .Include(x => x.Assembly)
                .ThenInclude(x => x.SubAssemblies)
                .ThenInclude(x => x.Parts)
                .First(x => x.Id == robotId);

            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

            var userRoles = this.UserManager.GetRolesAsync(user).GetAwaiter().GetResult();

            bool admin = userRoles.Any(x => x == GlobalConstants.AdminRole);
            bool owner = user.Id == robot.UserId;

            if (!admin && !owner)
            {
                throw new InvalidOperationException(GlobalConstants.UserHasNoRights);
            }

            this.Context.Remove(robot);
            if (robot.Assembly != null)
            {
                this.Context.Remove(robot.Assembly);
                if (robot.Assembly.SubAssemblies != null)
                {
                    this.Context.RemoveRange(robot.Assembly.SubAssemblies);
                    this.Context.RemoveRange(robot.Assembly.SubAssemblies.SelectMany(x => x.Parts));
                }
            }

            this.Context.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public RobotEditModel GetRobotToEdit(string robotId)
        {
            var robot = this.Context.Robots
                .Include(x => x.User)
                .Include(x => x.ImageUrl)
                .ProjectTo<RobotEditModel>(Mapper.ConfigurationProvider)
                .First(x => x.Id == robotId);

            return robot;
        }

        public int GetAssembliesCount(string id)
        {
            var count = this.Context.Robots
                .Where(x => x.Id == id)
                .Count(x => x.Assembly != null);

            return count;
        }

        public bool CheckUserIsOwnerOfRobot(string userId, string robotId)
        {
            var robot = this.Context.Robots
                .Include(x => x.User)
                .First(x => x.Id == robotId);

            bool isOwner = robot.UserId == userId;

            return isOwner;
        }

        public void EditRobot(RobotEditModel model)
        {
            var robot = this.Context.Robots
                .Include(x => x.User)
                .First(x => x.Id == model.Id);

            var cloudinary = SetCloudinary();

            var url = UploadImage(cloudinary, model.Image, model.Name).GetAwaiter().GetResult();

            robot.Name = model.Name;
            robot.SerialNumber = model.SerialNumber;
            robot.Axes = model.Axes;
            if (url != null)
            {
                robot.ImageUrl = url;
            }
           

            this.Context.Robots.Update(robot);
            this.Context.SaveChanges();
        }
    }
}
