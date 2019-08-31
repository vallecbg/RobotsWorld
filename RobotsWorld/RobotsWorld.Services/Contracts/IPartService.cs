using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RobotsWorld.Models;
using RobotsWorld.ViewModels.InputModels.Parts;

namespace RobotsWorld.Services.Contracts
{
    public interface IPartService
    {
        Task<string> Create(PartInputModel model);

        Part GetPartById(string partId);

        Task DeletePart(string subAssemblyId);
    }
}
