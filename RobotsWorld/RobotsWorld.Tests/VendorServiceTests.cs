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
    public class VendorServiceTests : BaseService
    {
        protected IVendorService vendorService => this.Provider.GetRequiredService<IVendorService>();

        [Test]
        public void GetAllVendorNames_Should_Succeed()
        {
            var vendors = new[]
            {
                new Vendor()
                {
                    Name = "vendor1"
                },
                new Vendor()
                {
                    Name = "vendor2"
                },
            };

            this.Context.Vendors.AddRange(vendors);
            this.Context.SaveChanges();

            var result = this.vendorService.GetAllVendorNames().First();

            result.Should().Be(vendors[0].Name);
        }

        [Test]
        public void GetVendorByName_Should_Succeed()
        {
            var vendor = new Vendor()
            {
                Name = "vendor1"
            };

            this.Context.Vendors.Add(vendor);
            this.Context.SaveChanges();

            var result = this.vendorService.GetVendorByName(vendor.Name);

            result.Should().NotBeNull()
                .And.Subject.As<Vendor>()
                .Name.Should().Be(vendor.Name);
        }

        [Test]
        public void CheckVendorIsValid_Should_Succeed()
        {
            var vendor = new Vendor()
            {
                Name = "vendor1"
            };

            this.Context.Vendors.Add(vendor);
            this.Context.SaveChanges();

            var result = this.vendorService.CheckVendorIsValid(vendor.Name);

            result.Should().BeTrue();
        }
    }
}
