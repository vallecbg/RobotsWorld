using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.Assemblies;

namespace RobotsWorld.Web.Controllers
{
    public class AssemblyController : Controller
    {
        private readonly IAssemblyService assemblyService;

        public AssemblyController(IAssemblyService assemblyService)
        {
            this.assemblyService = assemblyService;
        }

        [HttpGet]
        public IActionResult Create(string robotId)
        {
            var model = new AssemblyInputModel
            {
                Name = "",
                RobotId = robotId
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Create(AssemblyInputModel model)
        {
            this.assemblyService.Create(model);

            return RedirectToAction("Details", "Robots", new { id = model.RobotId });
        }
    }
}
