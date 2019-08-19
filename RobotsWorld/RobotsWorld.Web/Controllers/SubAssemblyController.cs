using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RobotsWorld.Services.Constants;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.SubAssemblies;

namespace RobotsWorld.Web.Controllers
{
    [Authorize]
    public class SubAssemblyController : Controller
    {
        private readonly ISubAssemblyService subAssemblyService;
        private readonly IAssemblyService assemblyService;

        public SubAssemblyController(ISubAssemblyService subAssemblyService, IAssemblyService assemblyService)
        {
            this.subAssemblyService = subAssemblyService;
            this.assemblyService = assemblyService;
        }

        [HttpGet]
        [Route(GlobalConstants.RouteConstants.CreateSubAssemblyRoute)]
        public IActionResult Create(string assemblyId)
        {
            this.ViewData[GlobalConstants.AssemblyId] = assemblyId;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubAssemblyInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.subAssemblyService.Create(model);

            var assembly = this.assemblyService.GetAssemblyById(model.AssemblyId);

            return this.RedirectToAction("Details", "Robots", new{id = assembly.RobotId});
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var subAssembly = this.subAssemblyService.GetSubAssemblyDetails(id);

            return this.View(subAssembly);
        }
    }
}
