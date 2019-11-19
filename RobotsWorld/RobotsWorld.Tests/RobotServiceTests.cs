using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RobotsWorld.Models;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.Robots;
using FluentAssertions;
using RobotsWorld.Services.Constants;
using RobotsWorld.ViewModels.OutputModels.Robots;

namespace RobotsWorld.Tests
{
    [TestFixture]
    public class RobotServiceTests : BaseService
    {
        protected IRobotService robotService => this.Provider.GetRequiredService<IRobotService>();
        protected UserManager<User> userManager => this.Provider.GetRequiredService<UserManager<User>>();
        protected RoleManager<IdentityRole> roleManager => this.Provider.GetRequiredService<RoleManager<IdentityRole>>();

        [Test]
        public void CreateRobot_With_No_Image_Should_Succeed()
        {
            var user = new User()
            {
                Id = "1",
                Name = "Gosho",
                UserName = "gosho123"
            };

            userManager.CreateAsync(user).GetAwaiter();
            this.Context.SaveChanges();

            var robotInputModel = new RobotInputModel
            {
                User = user.UserName,
                Image = null,
                Axes = 2,
                Name = "myRobot",
                SerialNumber = "24242"
            };

            var result = this.robotService.CreateRobot(robotInputModel).GetAwaiter().GetResult();

            var robot = this.Context.Robots.FirstOrDefault();

            result.Should().NotBeNull();
            robot.Should().NotBeNull()
                .And.Subject.Should().BeEquivalentTo(new
                {
                    Id = result,
                    ImageUrl = GlobalConstants.NoImageAvailableUrl,
                    Name = robot.Name
                }, options => options.ExcludingMissingMembers());
        }

        [Test]
        public void DeleteRobot_Should_Succeed()
        {
            var admin = new User()
            {
                Id = "123",
                Name = "Papuncho Kunchev",
                UserName = "admin"
            };
            var user = new User()
            {
                Id = "321",
                Name = "Kuncho Papunchev",
                UserName = "author"
            };

            var delivery = new Delivery()
            {
                Id = "1",
                Price = 2.5m,
                SentOn = DateTime.UtcNow
            };

            var robot = new Robot()
            {
                Id = "1",
                UserId = user.Id,
                Axes = 2,
                Name = "myRobot",
                SerialNumber = "2323",
                Deliveries = new List<Delivery>() { delivery }
            };

            userManager.CreateAsync(admin).GetAwaiter();
            userManager.CreateAsync(user).GetAwaiter();

            var role = new IdentityRole { Name = "admin" };
            roleManager.CreateAsync(role).GetAwaiter();
            userManager.AddToRoleAsync(admin, role.Name).GetAwaiter();

            this.Context.Robots.Add(robot);
            this.Context.SaveChanges();

            robotService.DeleteRobot(robot.Id, admin.UserName);

            var result = this.Context.Robots.FirstOrDefault();

            result.Should().BeNull();
        }

        [Test]
        public void DeleteRobot_Should_Succeed_With_All_Of_The_Nested_Classes()
        {
            var admin = new User()
            {
                Id = "123",
                Name = "Papuncho Kunchev",
                UserName = "admin"
            };
            var user = new User()
            {
                Id = "321",
                Name = "Kuncho Papunchev",
                UserName = "author"
            };

            var robot = new Robot()
            {
                Id = "1",
                UserId = user.Id,
                Axes = 2,
                Name = "myRobot",
                SerialNumber = "2323"
            };

            var assembly = new Assembly()
            {
                Id = "1",
                RobotId = robot.Id
            };

            var subAssembly = new SubAssembly()
            {
                AssemblyId = assembly.Id,
                Id = "1",
                Quantity = 1,
                Weight = 1,
                Name = "Motherboard"
            };

            var vendor = new Vendor()
            {
                Id = "1",
                Name = "IBM"
            };

            var part = new Part()
            {
                Id = "1",
                Price = 2.50m,
                Quantity = 2,
                SubAssemblyId = subAssembly.Id,
                VendorId = vendor.Id
            };

            userManager.CreateAsync(admin).GetAwaiter();
            userManager.CreateAsync(user).GetAwaiter();

            var role = new IdentityRole { Name = "admin" };
            roleManager.CreateAsync(role).GetAwaiter();
            userManager.AddToRoleAsync(admin, role.Name).GetAwaiter();

            this.Context.Robots.Add(robot);
            this.Context.Assemblies.Add(assembly);
            this.Context.SubAssemblies.Add(subAssembly);
            this.Context.Parts.Add(part);
            this.Context.Vendors.Add(vendor);
            this.Context.SaveChanges();

            robotService.DeleteRobot(robot.Id, admin.UserName);

            var result = this.Context.Robots.FirstOrDefault();

            result.Should().BeNull();
        }

        [Test]
        public void DeleteRobot_Should_Throw_Error_NoRights()
        {
            var user1 = new User()
            {
                Id = "123",
                Name = "Papuncho Kunchev",
                UserName = "user1"
            };
            var user2 = new User()
            {
                Id = "321",
                Name = "Kuncho Papunchev",
                UserName = "user2"
            };

            var robot = new Robot()
            {
                Id = "1",
                UserId = user2.Id,
                Axes = 2,
                Name = "myRobot",
                SerialNumber = "2323"
            };

            userManager.CreateAsync(user1).GetAwaiter();
            userManager.CreateAsync(user2).GetAwaiter();

            this.Context.Robots.Add(robot);
            this.Context.SaveChanges();

            Action act = () => robotService.DeleteRobot(robot.Id, user1.UserName);

            var message = GlobalConstants.UserHasNoRights;

            act.Should().Throw<InvalidOperationException>().WithMessage(message);
        }

        [Test]
        public void EditRobot_Should_Succeed()
        {
            var robot = new Robot()
            {
                Id = "1",
                UserId = "123",
                Axes = 2,
                Name = "myRobot",
                SerialNumber = "2323"
            };

            this.Context.Robots.Add(robot);
            this.Context.SaveChanges();

            var robotEditModel = new RobotEditModel()
            {
                Id = "1",
                Name = "changed",
                Axes = 4,
                SerialNumber = "3333",
                Image = null
            };

            this.robotService.EditRobot(robotEditModel);

            var result = this.Context.Robots.Find(robot.Id);

            result.Should().NotBeNull()
                .And.Subject.As<Robot>()
                .Name.Should().BeEquivalentTo(robotEditModel.Name);
        }



        [Test]
        public void GetUserRobots_Should_Succeed()
        {
            var user = new User()
            {
                Id = "123",
                Name = "Kuncho Papunchev",
                UserName = "author"
            };

            var robots = new[]
            {
                new Robot()
                {
                    Id = "1",
                    Axes = 2,
                    Name = "asdad",
                    SerialNumber = "2323223",
                    UserId = user.Id
                },
                new Robot()
                {
                    Id = "2",
                    Axes = 5,
                    Name = "eqweqw",
                    SerialNumber = "2222",
                    UserId = "55"
                },
            };

            userManager.CreateAsync(user).GetAwaiter();
            this.Context.Robots.AddRange(robots);
            this.Context.SaveChanges();

            var result = this.robotService.GetUserRobots(user.Id);

            result.Should().NotBeNull()
                .And.BeOfType<List<RobotOutputModel>>()
                .And.HaveCount(1);
        }

        [Test]
        public void GetRobotDetails_Should_Succeed()
        {
            var robot = new Robot()
            {
                Id = "1",
                UserId = "123",
                Axes = 2,
                Name = "myRobot",
                SerialNumber = "2323"
            };

            this.Context.Robots.Add(robot);
            this.Context.SaveChanges();

            var result = this.robotService.GetRobotDetails(robot.Id);

            result.Should().BeOfType<RobotOutputModel>()
                .And.Should().NotBeNull();
        }

        [Test]
        public void GetRobotToEdit_Should_Succeed()
        {
            var robot = new Robot()
            {
                Id = "1",
                UserId = "123",
                Axes = 2,
                Name = "myRobot",
                SerialNumber = "2323"
            };

            this.Context.Robots.Add(robot);
            this.Context.SaveChanges();

            var result = this.robotService.GetRobotToEdit(robot.Id);

            result.Should().NotBeNull()
                .And.BeOfType<RobotEditModel>();
        }

        [Test]
        public void GetAssembliesCount_Should_Succeed()
        {
            var robot = new Robot()
            {
                Id = "1",
                UserId = "123",
                Axes = 2,
                Name = "myRobot",
                SerialNumber = "2323",
                Assembly = new Assembly()
                {
                    Id = "1"
                }
            };

            this.Context.Robots.Add(robot);
            this.Context.SaveChanges();

            var result = this.robotService.GetAssembliesCount(robot.Id);

            result.Should().Be(1);
        }

        [Test]
        public void CheckUserIsOwnerOfRobot_Should_Succeed()
        {
            var user = new User()
            {
                Id = "123",
                Name = "Kuncho Papunchev",
                UserName = "author"
            };

            var robots = new[]
            {
                new Robot()
                {
                    Id = "1",
                    Axes = 2,
                    Name = "asdad",
                    SerialNumber = "2323223",
                    UserId = user.Id
                },
                new Robot()
                {
                    Id = "2",
                    Axes = 5,
                    Name = "eqweqw",
                    SerialNumber = "2222",
                    UserId = "55"
                },
            };

            userManager.CreateAsync(user).GetAwaiter();
            this.Context.Robots.AddRange(robots);
            this.Context.SaveChanges();

            var result = this.robotService.CheckUserIsOwnerOfRobot(user.Id, robots[0].Id);

            result.Should().BeTrue();
        }
    }
}
