using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RobotsWorld.Data;
using RobotsWorld.Models;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.Parts;

namespace RobotsWorld.Services
{
    public class PartService : BaseService, IPartService
    {
        private readonly IVendorService vendorService;
        private readonly ISubAssemblyService subAssemblyService;

        public PartService(UserManager<User> userManager, RobotsWorldContext context, IMapper mapper, IVendorService vendorService, ISubAssemblyService subAssemblyService) 
            : base(userManager, context, mapper)
        {
            this.vendorService = vendorService;
            this.subAssemblyService = subAssemblyService;
        }

        public async Task<string> Create(PartInputModel model)
        {
            var part = this.Mapper.Map<Part>(model);

            var vendor = this.vendorService.GetVendorByName(model.VendorName);
            var subAssembly = this.subAssemblyService.GetById(model.SubAssemblyId);

            part.VendorId = vendor.Id;
            vendor.Parts.Add(part);
            subAssembly.Parts.Add(part);

            this.Context.Parts.Add(part);
            this.Context.Vendors.Update(vendor);
            this.Context.SubAssemblies.Update(subAssembly);
            await this.Context.SaveChangesAsync();

            return part.Id;
        }

        public Part GetPartById(string partId)
        {
            var part = this.Context.Parts
                .Include(x => x.SubAssembly)
                .ThenInclude(x => x.Assembly)
                .ThenInclude(x => x.Robot)
                .Include(x => x.Vendor)
                .First(x => x.Id == partId);

            return part;
        }

        public async Task DeletePart(string partId)
        {
            var part = this.Context.Parts
                .Include(x => x.SubAssembly)
                .First(x => x.Id == partId);

            this.Context.Parts.Remove(part);
            await this.Context.SaveChangesAsync();
        }
    }
}
