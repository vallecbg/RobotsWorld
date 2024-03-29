﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RobotsWorld.Models;
using RobotsWorld.ViewModels.InputModels.Assemblies;

namespace RobotsWorld.Services.Contracts
{
    public interface IAssemblyService
    {
        Task<string> Create(string robotId);

        Assembly GetAssemblyById(string assemblyId);
    }
}
