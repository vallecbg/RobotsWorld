using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RobotsWorld.Services.Constants;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.Robots;

namespace RobotsWorld.Web.Controllers
{
    public class RobotsController : Controller
    {
        private readonly IRobotService robotService;

        public RobotsController(IRobotService robotService)
        {
            this.robotService = robotService;
        }

        [HttpGet]
        public IActionResult CreateRobot()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRobot(RobotInputModel model)
        {
            bool imageNotNull = model.Image != null;
            bool wrongType = false;

            if (imageNotNull)
            {
                var fileType = model.Image.ContentType.Split('/')[1];

                wrongType = GlobalConstants.ImageExtensions.Contains(fileType);
            }

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            if (!wrongType && imageNotNull)
            {
                this.ViewData[GlobalConstants.Error] = GlobalConstants.WrongFileType;
                return this.View(model);
            }

            var id = await this.robotService.CreateRobot(model);

            return Redirect("/");
            //TODO: Change it!
            //return RedirectToAction("Details", "Robot", new { id });
        }
    }
}
