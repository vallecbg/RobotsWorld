using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.Assemblies;

namespace RobotsWorld.Web.Controllers
{
    [Authorize]
    public class AssemblyController : Controller
    {
        private readonly IAssemblyService assemblyService;

        public AssemblyController(IAssemblyService assemblyService)
        {
            this.assemblyService = assemblyService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string robotId)
        {
            await this.assemblyService.Create(robotId);

            return RedirectToAction("Details", "Robots", new { id = robotId });
        }
    }
}
