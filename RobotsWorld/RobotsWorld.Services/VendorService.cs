using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RobotsWorld.Data;
using RobotsWorld.Models;
using RobotsWorld.Services.Contracts;

namespace RobotsWorld.Services
{
    public class VendorService : BaseService, IVendorService
    {
        public VendorService(UserManager<User> userManager, RobotsWorldContext context, IMapper mapper) : base(userManager, context, mapper)
        {
        }

        public ICollection<string> GetAllVendorNames()
        {
            var result = this.Context.Vendors
                .Select(x => x.Name)
                .ToList();

            return result;
        }

        public Vendor GetVendorByName(string vendorName)
        {
            var vendor = this.Context.Vendors
                .Include(x => x.Parts)
                .FirstOrDefault(x => x.Name == vendorName);

            return vendor;
        }
    }
}
