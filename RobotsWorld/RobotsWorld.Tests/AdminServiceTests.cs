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
using RobotsWorld.ViewModels.OutputModels.Users;

namespace RobotsWorld.Tests
{
    [TestFixture]
    public class AdminServiceTests : BaseService
    {
        protected UserManager<User> userManager =>
            this.Provider.GetRequiredService<UserManager<User>>();
        protected IAdminService adminService => this.Provider.GetRequiredService<IAdminService>();

        protected RoleManager<IdentityRole> roleService => this.Provider.GetRequiredService<RoleManager<IdentityRole>>();


        [Test]
        public void GetAllUsers_Should_Succeed()
        {
            var user = new User()
            {
                Id = "1",
                Name="Ivan ivanov",
                UserName = "dasdasd"
            };

            this.userManager.CreateAsync(user).GetAwaiter();
            this.Context.SaveChanges();

            var result = this.adminService.GetAllUsers().GetAwaiter().GetResult().First();

            result.Should().NotBeNull()
                .And.Subject.As<AdminUsersOutputModel>()
                .Name.Should().Be(user.Name);
        }

        [Test]
        public void DeleteUser_Should_Succeed()
        {
            var user = new User()
            {
                Id = "1",
                Name = "Ivan ivanov",
                UserName = "dasdasd"
            };

            this.userManager.CreateAsync(user).GetAwaiter();
            this.Context.SaveChanges();

            this.adminService.DeleteUser(user.Id);

            var userFromDb = this.Context.Users.FirstOrDefault();

            userFromDb.Should().BeNull();
        }

        [Test]
        public void DeleteUser_Should_Throw_Exception_User_NotFound()
        {
            Action act = () => this.adminService.DeleteUser("123").GetAwaiter().GetResult();

            var message = GlobalConstants.ErrorOnDeleteUser;

            act.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Test]
        public void GetAllVendors_Should_Succeed()
        {
            var vendors = new[]
            {
                new Vendor()
                {
                    Id = "1",
                    Name = "ibm"
                },
                new Vendor()
                {
                    Id = "2",
                    Name = "google"
                },
            };

            this.Context.Vendors.AddRange(vendors);
            this.Context.SaveChanges();

            var result = this.adminService.GetAllVendors();

            result.Should().NotBeEmpty()
                .And.HaveCount(vendors.Length);
        }

        [Test]
        public void AddVendor_Should_Succeed_And_Return_Success()
        {
            var vendorName = "test123";

            var result = this.adminService.AddVendor(vendorName).GetAwaiter().GetResult();

            result.Should().Be(GlobalConstants.Success);
        }

        [Test]
        public void AddVendor_Should_Return_Failed()
        {
            var vendorName = "test123";

            this.adminService.AddVendor(vendorName).GetAwaiter().GetResult();
            var result = this.adminService.AddVendor(vendorName).GetAwaiter().GetResult();

            result.Should().Be(GlobalConstants.Failed);
        }

        //[Test]
        //public void DeleteVendor_Should_Succeed()
        //{

        //}
    }
}
