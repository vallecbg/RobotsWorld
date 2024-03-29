﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RobotsWorld.Services.Constants;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.Robots;

namespace RobotsWorld.Web.Controllers
{
    [Authorize]
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


            return RedirectToAction("Details", "Robots", new { id });
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var result = this.robotService.GetRobotDetails(id);

            return this.View(result);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var username = this.User.Identity.Name;

            this.robotService.DeleteRobot(id, username);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var robotToEdit = this.robotService.GetRobotToEdit(id);

            if (robotToEdit == null)
            {
                return NotFound();
            }

            return this.View(robotToEdit);
        }

        [HttpPost]
        public IActionResult Edit(RobotEditModel model)
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

            this.robotService.EditRobot(model);

            return RedirectToAction("Details", "Robots", new {id = model.Id});
        }
    }
}
