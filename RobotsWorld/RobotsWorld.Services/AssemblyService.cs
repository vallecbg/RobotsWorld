using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RobotsWorld.Data;
using RobotsWorld.Models;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.Assemblies;

namespace RobotsWorld.Services
{
    public class AssemblyService : BaseService, IAssemblyService
    {
        public AssemblyService(UserManager<User> userManager, RobotsWorldContext context, IMapper mapper) : base(userManager, context, mapper)
        {
        }

        public string Create(AssemblyInputModel model)
        {
            var assembly = Mapper.Map<Assembly>(model);

            var robot = this.Context.Robots
                .Find(model.RobotId);

            assembly.Robots.Add(robot);
            this.Context.Assemblies.Add(assembly);
            this.Context.SaveChanges();

            robot.AssemblyId = assembly.Id;
            this.Context.Robots.Update(robot);
            this.Context.SaveChanges();

            return assembly.Id;
        }
    }
}
