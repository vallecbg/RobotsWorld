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
using RobotsWorld.ViewModels.OutputModels.Robots;
using RobotsWorld.ViewModels.OutputModels.TransportTypes;
using RobotsWorld.ViewModels.OutputModels.Users;

namespace RobotsWorld.Tests
{
    [TestFixture]
    public class AdminServiceTests : BaseService
    {
        protected UserManager<User> userManager =>
            this.Provider.GetRequiredService<UserManager<User>>();
        protected IAdminService adminService => this.Provider.GetRequiredService<IAdminService>();

        protected RoleManager<IdentityRole> roleManager => this.Provider.GetRequiredService<RoleManager<IdentityRole>>();


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

        [Test]
        public void DeleteVendor_Should_Succeed()
        {
            var vendor = new Vendor()
            {
                Id="1",
                Name="vendor1"
            };

            var admin = new User()
            {
                Id = "1",
                Name = "Ivan ivanov",
                UserName = "dasdasd"
            };

            this.userManager.CreateAsync(admin).GetAwaiter();

            var role = new IdentityRole { Name = "admin" };
            roleManager.CreateAsync(role).GetAwaiter();
            userManager.AddToRoleAsync(admin, role.Name).GetAwaiter();

            this.Context.Vendors.Add(vendor);
            this.Context.SaveChanges();

            var result = this.adminService.DeleteVendor(vendor.Id, admin.UserName);

            var vendorFromDb = this.Context.Vendors.FirstOrDefault();

            vendorFromDb.Should().BeNull();
        }

        [Test]
        public void DeleteVendor_Should_Return_No_Rights()
        {
            var vendor = new Vendor()
            {
                Id = "1",
                Name = "vendor1"
            };

            var user = new User()
            {
                Id = "1",
                Name = "Ivan ivanov",
                UserName = "dasdasd"
            };

            this.userManager.CreateAsync(user).GetAwaiter();

            this.Context.Vendors.Add(vendor);
            this.Context.SaveChanges();

            Action act = () => this.adminService.DeleteVendor(vendor.Id, user.UserName).GetAwaiter().GetResult();

            var message = GlobalConstants.UserHasNoRights;

            act.Should().Throw<OperationCanceledException>().WithMessage(message);
        }

        [Test]
        public void DeleteVendor_Should_Return_Vendor_NotFound()
        {
            var admin = new User()
            {
                Id = "1",
                Name = "Ivan ivanov",
                UserName = "dasdasd"
            };

            this.userManager.CreateAsync(admin).GetAwaiter();

            var role = new IdentityRole { Name = "admin" };
            roleManager.CreateAsync(role).GetAwaiter();
            userManager.AddToRoleAsync(admin, role.Name).GetAwaiter();
            this.Context.SaveChanges();

            Action act = () => this.adminService.DeleteVendor("111", admin.UserName).GetAwaiter().GetResult();

            var message = GlobalConstants.RecordDoesntExist;

            act.Should().Throw<InvalidOperationException>().WithMessage(message);
        }

        [Test]
        public void GetAllRobots_Should_Succeed()
        {
            var robots = new[]
            {
                new Robot()
                {
                    Id = "1",
                    Axes = 3,
                    Name = "myRobot1",
                    SerialNumber = "24242"
                },
                new Robot()
                {
                    Id = "2",
                    Axes = 3,
                    Name = "myRobot2",
                    SerialNumber = "45453"
                },
                new Robot()
                {
                    Id = "3",
                    Axes = 1,
                    Name = "myRobot3",
                    SerialNumber = "12356"
                },
            };

            this.Context.Robots.AddRange(robots);
            this.Context.SaveChanges();

            var result = this.adminService.GetAllRobots();

            var firstRobotFromDb = result.First();

            result.Should().HaveCount(robots.Length);
            firstRobotFromDb.Should().NotBeNull()
                .And.Subject.As<RobotAdminOutputModel>()
                .Name.Should().Be(robots[0].Name);
        }

        [Test]
        public void ChangeRole_Should_Succeed()
        {
            var roles = new[]
            {
                "admin",
                "user"
            };

            foreach (var currentRolename in roles)
            {
                var role = new IdentityRole
                {
                    Name = currentRolename
                };
                this.roleManager.CreateAsync(role).GetAwaiter();
            }

            var user = new User()
            {
                UserName = "user",
                Name = "Goshko Petkov"
            };

            this.userManager.CreateAsync(user).GetAwaiter();
            this.Context.SaveChanges();

            var newRole = roles[1];
            var model = new ChangingRoleModel
            {
                Id = user.Id,
                AppRoles = roles,
                NewRole = newRole,
                Name = user.Name,
                Role = GlobalConstants.DefaultRole
            };

            var result = this.adminService.ChangeRole(model);
            result.Should().Equals(IdentityResult.Success);
        }

        [Test]
        public void ChangeRole_Should_Throw_Exception_Role_NotFound()
        {
            var roles = new[]
            {
                "admin",
                "user"
            };

            foreach (var currentRolename in roles)
            {
                var role = new IdentityRole
                {
                    Name = currentRolename
                };
                this.roleManager.CreateAsync(role).GetAwaiter();
            }

            var user = new User()
            {
                UserName = "user",
                Name = "Goshko Petkov"
            };

            this.userManager.CreateAsync(user).GetAwaiter();
            this.Context.SaveChanges();

            var newRole = "asdasd";
            var model = new ChangingRoleModel
            {
                Id = user.Id,
                AppRoles = roles,
                NewRole = newRole,
                Name = user.Name,
                Role = GlobalConstants.DefaultRole
            };
            var result = this.adminService.ChangeRole(model);

            result.Should().Equals(IdentityResult.Failed());
        }

        [Test]
        public void AdminModifyRole_Should_Succeed()
        {
            var user = new User()
            {
                UserName = "user",
                Name = "Goshko Petkov"
            };

            this.userManager.CreateAsync(user).GetAwaiter();
            this.Context.SaveChanges();

            var roles = new[]
            {
                "admin",
                "user"
            };

            foreach (var currentRolename in roles)
            {
                var role = new IdentityRole
                {
                    Name = currentRolename
                };
                this.roleManager.CreateAsync(role).GetAwaiter();
            }

            var result = this.adminService.AdminModifyRole(user.Id);

            result.Should().Equals(IdentityResult.Success);
        }

        [Test]
        public void GetAllTransportTypes_Should_Succeed()
        {
            var transportTypes = new[]
            {
                new TransportType()
                {
                    Id = "1",
                    Name = "bus"
                },
                new TransportType()
                {
                    Id = "2",
                    Name = "train"
                },
                new TransportType()
                {
                    Id = "3",
                    Name = "plane"
                },
            };

            this.Context.TransportTypes.AddRange(transportTypes);
            this.Context.SaveChanges();

            var result = this.adminService.GetAllTransportTypes();

            var transportType = result.First();

            result.Should().HaveCount(transportTypes.Length);
            transportType.Should().NotBeNull()
                .And.Subject.As<TransportTypeOutputModel>()
                .Name.Should().Be(transportTypes[0].Name);
        }

        [Test]
        public void AddTransportType_Should_Succeed()
        {
            var transportTypeName = "transport1";

            var result = this.adminService.AddTransportType(transportTypeName).GetAwaiter().GetResult();

            var transportFromDb = this.Context.TransportTypes.First();

            result.Should().Be(GlobalConstants.Success);
            transportFromDb.Should().NotBeNull()
                .And.Subject.Should().BeEquivalentTo(new
                {
                    Name = transportTypeName
                }, options => options.ExcludingMissingMembers());
        }

        [Test]
        public void AddTransportType_Should_Throw_Error_Duplicate()
        {
            var transportTypeInput = new TransportType()
            {
                Id = "1",
                Name = "asdasd"
            };
            this.Context.TransportTypes.Add(transportTypeInput);
            this.Context.SaveChanges();

            var result = this.adminService.AddTransportType(transportTypeInput.Name).GetAwaiter().GetResult();

            result.Should().Be(GlobalConstants.Failed);
        }

        [Test]
        public void DeleteTransportType_Should_Succeed()
        {
            var transportTypeInput = new TransportType()
            {
                Id = "1",
                Name = "asdasd"
            };
            this.Context.TransportTypes.Add(transportTypeInput);

            var admin = new User()
            {
                Id = "1",
                Name = "Ivan ivanov",
                UserName = "dasdasd"
            };

            this.userManager.CreateAsync(admin).GetAwaiter();

            var role = new IdentityRole { Name = "admin" };
            roleManager.CreateAsync(role).GetAwaiter();
            userManager.AddToRoleAsync(admin, role.Name).GetAwaiter();
            this.Context.SaveChanges();

            this.adminService.DeleteTransportType(transportTypeInput.Id, admin.UserName).GetAwaiter();

            var transportTypeFromDb = this.Context.TransportTypes.FirstOrDefault();

            transportTypeFromDb.Should().BeNull();
        }

        [Test]
        public void DeleteTransportType_Should_Throw_Error_NoRights()
        {
            var transportTypeInput = new TransportType()
            {
                Id = "1",
                Name = "asdasd"
            };
            this.Context.TransportTypes.Add(transportTypeInput);

            var admin = new User()
            {
                Id = "1",
                Name = "Ivan ivanov",
                UserName = "dasdasd"
            };

            this.userManager.CreateAsync(admin).GetAwaiter();
            this.Context.SaveChanges();

            Action act = () => this.adminService.DeleteTransportType(transportTypeInput.Id, admin.UserName).GetAwaiter().GetResult();

            var message = GlobalConstants.UserHasNoRights;

            act.Should().Throw<OperationCanceledException>().WithMessage(message);
        }

        [Test]
        public void DeleteTransportType_Should_Throw_Error_TransportType_NotFound()
        {

            var admin = new User()
            {
                Id = "1",
                Name = "Ivan ivanov",
                UserName = "dasdasd"
            };

            this.userManager.CreateAsync(admin).GetAwaiter();

            var role = new IdentityRole { Name = "admin" };
            roleManager.CreateAsync(role).GetAwaiter();
            userManager.AddToRoleAsync(admin, role.Name).GetAwaiter();
            this.Context.SaveChanges();

            Action act = () => this.adminService.DeleteTransportType("111", admin.UserName).GetAwaiter().GetResult();

            var message = GlobalConstants.RecordDoesntExist;

            act.Should().Throw<InvalidOperationException>().WithMessage(message);
        }

        [Test]
        public void GetUsersCount_Should_Succeed()
        {
            var user1 = new User()
            {
                Id = "1",
                Name = "Ivan ivanov",
                UserName = "eqwe3qwe"
            };
            var user2 = new User()
            {
                Id = "2",
                Name = "Ivan ivanov",
                UserName = "asdasdasd"
            };

            this.userManager.CreateAsync(user1).GetAwaiter();
            this.userManager.CreateAsync(user2).GetAwaiter();
            this.Context.SaveChanges();

            var result = this.adminService.GetUsersCount();

            result.Should().Be(2);
        }

        [Test]
        public void GetRobotsCount_Should_Succeed()
        {
            var robots = new[]
            {
                new Robot()
                {
                    Id = "1",
                    Name = "asdasd"
                },
                new Robot()
                {
                    Id = "2",
                    Name = "qweqwe"
                },
                new Robot()
                {
                    Id = "3",
                    Name = "erwt34"
                },
            };

            this.Context.Robots.AddRange(robots);
            this.Context.SaveChanges();

            var result = this.adminService.GetRobotsCount();

            result.Should().Be(robots.Length);
        }

        [Test]
        public void GetVendorsCount_Should_Succeed()
        {
            var vendors = new[]
            {
                new Vendor()
                {
                    Id = "1",
                    Name = "vendor1"
                },
                new Vendor()
                {
                    Id = "2",
                    Name = "vendor2"
                },
                new Vendor()
                {
                    Id = "3",
                    Name = "vendor3"
                },
            };

            this.Context.Vendors.AddRange(vendors);
            this.Context.SaveChanges();

            var result = this.adminService.GetVendorsCount();

            result.Should().Be(vendors.Length);
        }

        [Test]
        public void GetTransportTypesCount_Should_Succeed()
        {
            var transportTypes = new[]
            {
                new TransportType()
                {
                    Id = "1",
                    Name = "transport1"
                },
                new TransportType()
                {
                    Id = "2",
                    Name = "transport2"
                },
                new TransportType()
                {
                    Id = "3",
                    Name = "transport3"
                },
            };

            this.Context.TransportTypes.AddRange(transportTypes);
            this.Context.SaveChanges();

            var result = this.adminService.GetTransportTypesCount();

            result.Should().Be(transportTypes.Length);
        }

        [Test]
        public void GetDeliveriesForAWeek_Should_Succeed()
        {
            var deliveries = new[]
            {
                new Delivery()
                {
                    Id = "1",
                    SentOn = DateTime.Now.AddDays(-2)
                },
                new Delivery()
                {
                    Id = "2",
                    SentOn = DateTime.Now.AddDays(-9)
                },
                new Delivery()
                {
                    Id = "3",
                    SentOn = DateTime.Now.AddDays(5)
                },
                new Delivery()
                {
                    Id = "4",
                    SentOn = DateTime.Now.AddDays(9)
                },
            };

            this.Context.Deliveries.AddRange(deliveries);
            this.Context.SaveChanges();

            var result = this.adminService.GetDeliveriesForAWeek();

            result.Should().NotBeEmpty();
        }

        [Test]
        public void GetTop3RobotsWithMostDeliveries_Should_Succeed()
        {
            var robots = new[]
            {
                new Robot()
                {
                    Id = "1",
                    Name="myRobot1",
                    Deliveries = new List<Delivery>()
                    {
                        new Delivery()
                        {
                            Id = "1",
                            SentOn = DateTime.Now.AddDays(-2)
                        },
                        new Delivery()
                        {
                            Id = "2",
                            SentOn = DateTime.Now.AddDays(3)
                        },
                        new Delivery()
                        {
                            Id = "3",
                            SentOn = DateTime.Now.AddDays(-2)
                        },
                    }
                },
                new Robot()
                {
                    Id = "2",
                    Name="myRobot2",
                    Deliveries = new List<Delivery>()
                    {
                        new Delivery()
                        {
                            Id = "4",
                            SentOn = DateTime.Now.AddDays(-2)
                        },
                        new Delivery()
                        {
                            Id = "5",
                            SentOn = DateTime.Now.AddDays(3)
                        },
                    }
                },
                new Robot()
                {
                    Id = "3",
                    Name="myRobot3",
                    Deliveries = new List<Delivery>()
                    {
                        new Delivery()
                        {
                            Id = "6",
                            SentOn = DateTime.Now.AddDays(-2)
                        },
                    }
                },
            };

            this.Context.Robots.AddRange(robots);
            this.Context.SaveChanges();

            var result = adminService.GetTop3RobotsWithMostDeliveries();

            result.Should().NotBeNull();
        }
    }
}
