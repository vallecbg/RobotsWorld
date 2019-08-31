using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RobotsWorld.Models;
using RobotsWorld.Services.Constants;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.Users;
using RobotsWorld.ViewModels.OutputModels.Users;

namespace RobotsWorld.Tests
{
    public class UserServiceTests : BaseService
    {
        protected UserManager<User> userManager =>
            this.Provider.GetRequiredService<UserManager<User>>();
        protected IUserService userService => this.Provider.GetRequiredService<IUserService>();
        protected RoleManager<IdentityRole> roleService => this.Provider.GetRequiredService<RoleManager<IdentityRole>>();



        [Test]
        public void Login_Return_Success()
        {
            var user = new User()
            {
                UserName = "vankata",
                Name = "Ivan Prakashev"
            };
            var password = "123456";

            this.userManager.CreateAsync(user).GetAwaiter().GetResult();
            this.userManager.AddPasswordAsync(user, password).GetAwaiter().GetResult();
            this.Context.SaveChanges();

            var loginUser = new LoginInputModel
            {
                Username = user.UserName,
                Password = password
            };

            var result = this.userService.Login(loginUser);

            result.Should().BeEquivalentTo(SignInResult.Success);
        }

        [Test]
        public void Login_Should_Fail_User_NotFound()
        {
            var username = "goshko";
            var password = "123456";

            var loginUser = new LoginInputModel()
            {
                Username = username,
                Password = password
            };

            var result = this.userService.Login(loginUser);

            result.Should().BeEquivalentTo(SignInResult.Failed);
        }

        [Test]
        public void Register_Should_Success()
        {
            var registerModel = new RegisterInputModel()
            {
                Username = "vankata",
                Name = "Ivan Goshkov",
                Password = "123456",
                ConfirmPassword = "123456",
                Email = "vankata@abv.bg"
            };

            this.roleService.CreateAsync(new IdentityRole { Name = GlobalConstants.DefaultRole }).GetAwaiter().GetResult();
            var result = this.userService.Register(registerModel).GetAwaiter().GetResult();

            result.Should().BeEquivalentTo(SignInResult.Success);
        }

        [Test]
        public void Register_Should_Fail_Duplicate_Username()
        {
            var registerModel = new RegisterInputModel()
            {
                Username = "vankata",
                Name = "Ivan Goshkov",
                Password = "123456",
                ConfirmPassword = "123456",
                Email = "vankata@abv.bg"
            };

            var user = new User()
            {
                UserName = "vankata",
                Name = "Goshko Ivanov",
                Email = "vankat@abv.bg"
            };

            this.userManager.CreateAsync(user).GetAwaiter();
            this.Context.SaveChanges();

            this.roleService.CreateAsync(new IdentityRole { Name = GlobalConstants.DefaultRole }).GetAwaiter();
            var result = this.userService.Register(registerModel).GetAwaiter().GetResult();

            result.Should().BeEquivalentTo(SignInResult.Failed);
        }

        [Test]
        public void GetName_Should_Return_Correct_Name()
        {
            var user = new User()
            {
                UserName = "goshko1",
                Name = "Ivan Goshkov1",
                Email = "vankat1@abv.bg"
            };

            this.userManager.CreateAsync(user).GetAwaiter();
            this.Context.SaveChanges();

            var result = this.userService.GetName(user.Id);

            result.Should().NotBeNull()
                .And.Subject.Should().Equals(user.Name);
        }

        [Test]
        public void Logout_Success()
        {
            this.userService.Logout();
        }

        [Test]
        public void GetUserDetails_Should_Succeed()
        {
            var user = new User()
            {
                UserName = "goshko1",
                Name = "Ivan Goshkov1",
                Email = "vankat1@abv.bg"
            };

            this.userManager.CreateAsync(user).GetAwaiter();
            this.Context.SaveChanges();

            var result = this.userService.GetUserDetails(user.Id);

            result.Should().NotBeNull()
                .And.BeOfType<UserOutputModel>();
        }

        [Test]
        public void GetAllChatroomMessages_Should_Succeed()
        {
            var user = new User()
            {
                Id="1",
                UserName = "gosho"
            };

            var chatroomMessages = new[]
            {
                new ChatRoomMessage()
                {
                    Id = "1",
                    Username = user.UserName,
                    Content = "asdasdasda",
                    PublishedOn = DateTime.UtcNow.AddDays(1)
                },
                new ChatRoomMessage()
                {
                    Id = "2",
                    Username = user.UserName,
                    Content = "qqweqweqw",
                    PublishedOn = DateTime.UtcNow.AddHours(1)
                },
                new ChatRoomMessage()
                {
                    Id = "3",
                    Username = user.UserName,
                    Content = "134354657687",
                    PublishedOn = DateTime.UtcNow
                },
            };

            this.userManager.CreateAsync(user).GetAwaiter();
            this.Context.ChatRoomMessages.AddRange(chatroomMessages);
            this.Context.SaveChanges();

            var result = this.userService.GetAllChatroomMessages();

            result.Should().HaveCount(chatroomMessages.Length);
        }
    }
}
