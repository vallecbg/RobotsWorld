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
        private readonly IVendorService vendorService;

        public PartController(IPartService partService, IVendorService vendorService)
        {
            this.partService = partService;
            this.vendorService = vendorService;
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
            bool checkVendorIsValid = this.vendorService.CheckVendorIsValid(model.VendorName);
            if (!this.ModelState.IsValid || !checkVendorIsValid)
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
