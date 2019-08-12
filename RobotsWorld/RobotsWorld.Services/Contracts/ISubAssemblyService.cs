using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RobotsWorld.ViewModels.InputModels.SubAssemblies;

namespace RobotsWorld.Services.Contracts
{
    public interface ISubAssemblyService
    {
        Task<string> Create(SubAssemblyInputModel model);
    }
}
