using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RobotsWorld.ViewModels.InputModels.Assemblies;

namespace RobotsWorld.Services.Contracts
{
    public interface IAssemblyService
    {
        string Create(string robotId);
    }
}
