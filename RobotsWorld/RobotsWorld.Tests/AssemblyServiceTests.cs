using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RobotsWorld.Models;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.Assemblies;

namespace RobotsWorld.Tests
{
    [TestFixture]
    public class AssemblyServiceTests : BaseService
    {
        protected IRobotService robotService => this.Provider.GetRequiredService<IRobotService>();
        protected IAssemblyService assemblyService => this.Provider.GetRequiredService<IAssemblyService>();
        protected UserManager<User> userManager => this.Provider.GetRequiredService<UserManager<User>>();
        protected RoleManager<IdentityRole> roleManager => this.Provider.GetRequiredService<RoleManager<IdentityRole>>();

        [Test]
        public void CreateAssembly_Should_Succeed()
        {
            var robot = new Robot()
            {
                Id = "1",
                UserId = "111",
                Axes = 2,
                Name = "myRobot",
                SerialNumber = "2323"
            };

            this.Context.Robots.Add(robot);
            this.Context.SaveChanges();

            var assembly = new AssemblyInputModel
            {
                Name = "asdasd",
                RobotId = robot.Id
            };

            var result = this.assemblyService.Create(robot.Id).GetAwaiter().GetResult();

            var assemblyResult = this.Context.Assemblies.First();

            result.Should().NotBeEmpty()
                .And.Be(assemblyResult.Id);
        }

        [Test]
        public void GetAssemblyById_Should_Succeed()
        {
            var robot = new Robot()
            {
                Id = "1",
                UserId = "111",
                Axes = 2,
                Name = "myRobot",
                SerialNumber = "2323"
            };

            var assembly = new Assembly()
            {
                Id="1",
                RobotId = robot.Id
            };

            this.Context.Robots.Add(robot);
            this.Context.Assemblies.Add(assembly);
            this.Context.SaveChanges();

            var result = this.assemblyService.GetAssemblyById(assembly.Id);

            result.Should().NotBeNull()
                .And.Subject.As<Assembly>()
                .RobotId.Should().Be(robot.Id);
        }
    }
}
