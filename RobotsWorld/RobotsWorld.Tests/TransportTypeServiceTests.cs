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

namespace RobotsWorld.Tests
{
    [TestFixture]
    public class TransportTypeServiceTests : BaseService
    {
        protected ITransportTypeService transportTypeService => this.Provider.GetRequiredService<ITransportTypeService>();

        [Test]
        public void GetAllTransportTypes_Should_Succeed()
        {
            var transportTypes = new[]
            {
                new TransportType()
                {
                    Name = "transport1"
                },
                new TransportType()
                {
                    Name = "transport2"
                },
                new TransportType()
                {
                    Name = "transport3"
                },
            };

            this.Context.TransportTypes.AddRange(transportTypes);
            this.Context.SaveChanges();

            var result = this.transportTypeService.GetAllTransportTypes().First();

            result.Should().NotBeNullOrWhiteSpace()
                .And.Subject.Should().Be(transportTypes[0].Name);
        }

        [Test]
        public void GetTransportTypeByName_Should_Succeed()
        {
            var transportTypes = new[]
            {
                new TransportType()
                {
                    Name = "transport1"
                },
                new TransportType()
                {
                    Name = "transport2"
                },
                new TransportType()
                {
                    Name = "transport3"
                },
            };

            this.Context.TransportTypes.AddRange(transportTypes);
            this.Context.SaveChanges();

            var result = this.transportTypeService.GetTransportTypeByName(transportTypes[0].Name);

            result.Should().NotBeNull()
                .And.Subject.As<TransportType>()
                .Name.Should().Be(transportTypes[0].Name);
        }
    }
}
