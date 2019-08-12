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
using RobotsWorld.ViewModels.InputModels.Assemblies;

namespace RobotsWorld.Services
{
    public class AssemblyService : BaseService, IAssemblyService
    {
        public AssemblyService(UserManager<User> userManager, RobotsWorldContext context, IMapper mapper) : base(userManager, context, mapper)
        {
        }

        public async Task<string> Create(string robotId)
        {
            var assembly = new Assembly();

            var robot = this.Context.Robots
                .Find(robotId);

            assembly.RobotId = robot.Id;
            robot.Assembly = assembly;

            this.Context.Assemblies.Add(assembly);
            this.Context.Robots.Update(robot);
            await this.Context.SaveChangesAsync();

            return assembly.Id;
        }

        public Assembly GetAssemblyById(string assemblyId)
        {
            var assembly = this.Context.Assemblies
                .Include(x => x.SubAssemblies)
                .Include(x => x.Robot)
                .First(x => x.Id == assemblyId);

            return assembly;
        }
    }
}
