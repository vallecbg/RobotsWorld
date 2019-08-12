using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RobotsWorld.Services.Constants;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.Parts;

namespace RobotsWorld.Web.Controllers
{
    [Authorize]
    public class PartController : Controller
    {
        private readonly IPartService partService;

        public PartController(IPartService partService)
        {
            this.partService = partService;
        }

        [HttpGet]
        [Route(GlobalConstants.RouteConstants.CreatePartRoute)]
        public IActionResult Create(string subAssemblyId)
        {
            this.ViewData[GlobalConstants.SubAssemblyId] = subAssemblyId;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var partId = this.partService.Create(model).Result;

            var part = this.partService.GetPartById(partId);

            var robotId = part.SubAssembly.Assembly.RobotId;

            return this.RedirectToAction("Details", "Robots", new {id = robotId});
        }
    }
}
