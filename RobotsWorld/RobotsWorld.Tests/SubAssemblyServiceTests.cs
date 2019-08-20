using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RobotsWorld.Models;
using RobotsWorld.Services.Constants;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.SubAssemblies;
using RobotsWorld.ViewModels.OutputModels.SubAssemblies;

namespace RobotsWorld.Tests
{
    [TestFixture]
    public class SubAssemblyServiceTests : BaseService
    {
        protected ISubAssemblyService subAssemblyService => this.Provider.GetRequiredService<ISubAssemblyService>();
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

            var assembly = new Assembly()
            {
                Id = "1",
                RobotId = robot.Id
            };

            this.Context.Robots.Add(robot);
            this.Context.Assemblies.Add(assembly);
            this.Context.SaveChanges();

            var subAssemblyInputModel = new SubAssemblyInputModel
            {
                AssemblyId = assembly.Id,
                Image = null,
                Name = "subassembly",
                Quantity = 3,
                Weight = 2.55
            };

            var result = this.subAssemblyService.Create(subAssemblyInputModel).GetAwaiter().GetResult();

            var subassembly = this.Context.SubAssemblies.Find(result);

            result.Should().NotBeNull();
            subassembly.Should().NotBeNull()
                .And.Subject.Should().BeEquivalentTo(new
                {
                    Id = result,
                    ImageUrl = GlobalConstants.NoImageAvailableUrl,
                    Name = subAssemblyInputModel.Name
                }, options => options.ExcludingMissingMembers());
        }

        [Test]
        public void GetById_Should_Succeed()
        {
            var subAssembly = new SubAssembly()
            {
                Id="1",
                AssemblyId = "1",
                Name = "subasdas",
                Quantity = 2,
                Weight = 3.50
            };

            this.Context.SubAssemblies.Add(subAssembly);
            this.Context.SaveChanges();

            var result = this.subAssemblyService.GetById(subAssembly.Id);

            result.Should().NotBeNull()
                .And.Subject.As<SubAssembly>()
                .Name.Should().Be(subAssembly.Name);
        }

        [Test]
        public void GetSubAssemblyDetails_Should_Succeed()
        {
            var subAssembly = new SubAssembly()
            {
                Id = "1",
                AssemblyId = "1",
                Name = "subasdas",
                Quantity = 2,
                Weight = 3.50
            };

            this.Context.SubAssemblies.Add(subAssembly);
            this.Context.SaveChanges();

            var result = this.subAssemblyService.GetSubAssemblyDetails(subAssembly.Id);

            result.Should().NotBeNull()
                .And.BeOfType<SubAssemblyDetailsOutputModel>();
        }
    }
}
