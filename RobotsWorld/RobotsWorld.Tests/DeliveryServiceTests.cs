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
using RobotsWorld.ViewModels.InputModels.Deliveries;
using RobotsWorld.ViewModels.OutputModels.Deliveries;

namespace RobotsWorld.Tests
{
    [TestFixture]
    public class DeliveryServiceTests : BaseService
    {
        protected UserManager<User> userManager =>
            this.Provider.GetRequiredService<UserManager<User>>();
        protected IDeliveryService deliveryService => this.Provider.GetRequiredService<IDeliveryService>();

        [Test]
        public void CreateDelivery_Should_Succeed()
        {
            var sender = new User()
            {
                Id="1",
                Name="Ivan Ivanov",
                UserName = "vankata"
            };

            var receiver = new User()
            {
                Id = "2",
                Name = "Gosho Goshov",
                UserName = "goshkata"
            };

            var robot = new Robot()
            {
                Id="1",
                Name = "myrobot",
                SerialNumber = "232323",
                UserId = sender.Id
            };

            var transportType = new TransportType()
            {
                Id="4",
                Name = "bus"
            };

            this.userManager.CreateAsync(sender).GetAwaiter();
            this.userManager.CreateAsync(receiver).GetAwaiter();
            this.Context.Robots.Add(robot);
            this.Context.TransportTypes.Add(transportType);
            this.Context.SaveChanges();

            var deliveryInput = new DeliveryInputModel
            {
                StartingPoint = "Stara Zagora",
                DestinationPoint = "Plovdiv",
                Price = 2.50m,
                RobotId = robot.Id,
                SenderId = sender.Id,
                ReceiverUsername = receiver.UserName,
                TransportTypeName = transportType.Name
            };

            var result = this.deliveryService.Create(deliveryInput);

            var delivery = this.Context.Deliveries.First();

            result.Should().NotBeNull();
            delivery.Should().NotBeNull()
                .And.Subject.Should().BeEquivalentTo(new
                {
                    Id = result,
                    SenderId = sender.Id,
                    ReceiverId = receiver.Id,
                    RobotId = robot.Id,
                    TransportTypeId = transportType.Id
                }, options => options.ExcludingMissingMembers());
        }

        [Test]
        public void GetDeliveryDetails_Should_Succeed()
        {
            var delivery = new Delivery()
            {
                Id="1",
                Price = 2.50m,
                ReceiverId = "123",
                SenderId = "321",
                RobotId = "1111",
                SentOn = DateTime.UtcNow
            };

            this.Context.Deliveries.Add(delivery);
            this.Context.SaveChanges();

            var result = this.deliveryService.GetDeliveryDetails(delivery.Id);

            result.Should().NotBeNull()
                .And.Subject.Should().BeOfType<DeliveryOutputModel>();
        }
    }
}
