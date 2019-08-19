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
using RobotsWorld.Services.Constants;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.SubAssemblies;
using RobotsWorld.ViewModels.OutputModels.SubAssemblies;

namespace RobotsWorld.Services
{
    public class SubAssemblyService : BaseService, ISubAssemblyService
    {
        public SubAssemblyService(UserManager<User> userManager, RobotsWorldContext context, IMapper mapper)
            : base(userManager, context, mapper)
        {
        }

        public async Task<string> Create(SubAssemblyInputModel model)
        {
            var cloudinary = SetCloudinary();

            var url = await UploadImage(cloudinary, model.Image, model.Name);

            var subAssembly = Mapper.Map<SubAssembly>(model);

            subAssembly.ImageUrl = url ?? GlobalConstants.NoImageAvailableUrl;

            this.Context.SubAssemblies.Add(subAssembly);
            await this.Context.SaveChangesAsync();

            return subAssembly.Id;
        }

        public SubAssembly GetById(string subAssemblyId)
        {
            var subAssembly = this.Context.SubAssemblies
                .Include(x => x.Assembly)
                .Include(x => x.Parts)
                .First(x => x.Id == subAssemblyId);

            return subAssembly;
        }

        public SubAssemblyDetailsOutputModel GetSubAssemblyDetails(string id)
        {
            var subAssembly = this.Context.SubAssemblies
                .Include(x => x.Parts)
                .FirstOrDefault(x => x.Id == id);

            var subAssemblyModel = this.Mapper.Map<SubAssemblyDetailsOutputModel>(subAssembly);

            return subAssemblyModel;
        }
    }
}
