using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RobotsWorld.Models;
using RobotsWorld.Services.Constants;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.Parts;

namespace RobotsWorld.Tests
{
    [TestFixture]
    public class PartServiceTests : BaseService
    {
        protected IPartService partService => this.Provider.GetRequiredService<IPartService>();

        [Test]
        public void CreatePart_Should_Succeed()
        {
            var subAssembly = new SubAssembly()
            {
                Id = "1",
                Name = "aasdasd",
                Quantity = 1,
                Weight = 1
            };

            var vendor = new Vendor()
            {
                Id="1",
                Name = "vendor"
            };

            this.Context.SubAssemblies.Add(subAssembly);
            this.Context.Vendors.Add(vendor);
            this.Context.SaveChanges();

            var partInputModel = new PartInputModel
            {
                Name = "part1",
                Price = 2.50m,
                Quantity = 2,
                SubAssemblyId = subAssembly.Id,
                VendorName = vendor.Name
            };

            var result = this.partService.Create(partInputModel).GetAwaiter().GetResult();

            var part = this.Context.Parts.First();

            result.Should().NotBeNull();
            part.Should().NotBeNull()
                .And.Subject.Should().BeEquivalentTo(new
                {
                    Id = result,
                    ImageUrl = GlobalConstants.NoImageAvailableUrl,
                    Name = partInputModel.Name
                }, options => options.ExcludingMissingMembers());
        }

        [Test]
        public void GetPartById_Should_Succeed()
        {
            var part = new Part()
            {
                Id = "1",
                Name = "asdasd",
                Price = 2.50m,
                Quantity = 3
            };

            this.Context.Parts.Add(part);
            this.Context.SaveChanges();

            var result = this.partService.GetPartById(part.Id);

            result.Should().NotBeNull()
                .And.Subject.As<Part>()
                .Name.Should().Be(part.Name);
        }

        [Test]
        public void DeletePart_Should_Succeed()
        {
            var part = new Part()
            {
                Id = "1",
                Name = "asdasd",
                Price = 2.50m,
                Quantity = 2,
                SubAssemblyId = "2323",
                VendorId = "222"
            };

            this.Context.Parts.Add(part);
            this.Context.SaveChanges();

            var result = this.partService.DeletePart(part.Id);

            var partFromDb = this.Context.Parts.FirstOrDefault();

            partFromDb.Should().BeNull();
        }
    }
}
