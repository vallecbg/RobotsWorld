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
                this.ViewData[GlobalConstants.SubAssemblyId] = model.SubAssemblyId;
                return this.View();
            }

            var partId = await this.partService.Create(model);

            var part = this.partService.GetPartById(partId);

            var robotId = part.SubAssembly.Assembly.RobotId;

            return this.RedirectToAction("Details", "Robots", new {id = robotId});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PartDeleteModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("Details", "SubAssembly", new {id = model.SubAssemblyId});
            }

            await this.partService.DeletePart(model.SubAssemblyId);

            return this.RedirectToAction("Details", "SubAssembly", new { id = model.SubAssemblyId });
        }
    }
}
