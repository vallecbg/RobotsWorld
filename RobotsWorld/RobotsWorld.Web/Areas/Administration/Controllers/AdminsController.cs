﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RobotsWorld.Services.Constants;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.OutputModels.Users;

namespace RobotsWorld.Web.Areas.Administration.Controllers
{
    [Area(GlobalConstants.RouteConstants.Administration)]
    [Authorize(Roles = GlobalConstants.Admin)]
    public class AdminsController : Controller
    {
        private readonly IAdminService adminService;
        private readonly IRobotService robotService;

        public AdminsController(IAdminService adminService, IRobotService robotService)
        {
            this.adminService = adminService;
            this.robotService = robotService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Users()
        {
            var users = this.adminService.GetAllUsers().Result;

            return this.View(users);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await this.adminService.DeleteUser(id);

            return RedirectToAction("Users");
        }

        [HttpGet]
        public IActionResult EditRole(string id)
        {
            var model = this.adminService.AdminModifyRole(id);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult EditRole(ChangingRoleModel model)
        {
            var result = this.adminService.ChangeRole(model).Result;

            if (result == IdentityResult.Success)
            {
                return RedirectToAction("Users");
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult Vendors()
        {
            var vendorsModel = this.adminService.GetAllVendors();

            return this.View(vendorsModel);
        }

        [HttpPost]
        public IActionResult Vendors(string vendorName)
        {
            var vendorsModel = this.adminService.GetAllVendors();

            if (string.IsNullOrEmpty(vendorName))
            {
                this.ViewData[GlobalConstants.Error] = GlobalConstants.NullName;
                return this.View(vendorsModel);
            }

            if (vendorsModel.Any(x => x.Name == vendorName))
            {
                this.ViewData[GlobalConstants.Error] = GlobalConstants.VendorNameDuplicate;
                return this.View(vendorsModel);
            }

            var result = this.adminService.AddVendor(vendorName);

            if (result.Result != GlobalConstants.Success)
            {
                string error = string.Format(GlobalConstants.VendorNameDuplicate);
                this.ViewData[GlobalConstants.Error] = error;
                return this.View(vendorsModel);
            }

            return this.RedirectToAction("Vendors");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteVendor(string id)
        {
            if (!this.User.IsInRole(GlobalConstants.Admin))
            {
                return RedirectToAction("Error", "Home", "");
            }

            var username = this.User.Identity.Name;

            await this.adminService.DeleteVendor(id, username);

            return RedirectToAction("Vendors", "Admins");
        }

        [HttpGet]
        public IActionResult Robots()
        {
            var robotsModel = this.adminService.GetAllRobots();

            return this.View(robotsModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRobot(string id)
        {
            var username = this.User.Identity.Name;

            this.robotService.DeleteRobot(id, username);

            return RedirectToAction("Robots");
        }

        [HttpGet]
        public IActionResult TransportTypes()
        {
            var transportTypesModel = this.adminService.GetAllTransportTypes();

            return this.View(transportTypesModel);
        }

        [HttpPost]
        public IActionResult TransportTypes(string transportTypeName)
        {
            var transportTypeModel = this.adminService.GetAllTransportTypes();

            if (string.IsNullOrEmpty(transportTypeName))
            {
                this.ViewData[GlobalConstants.Error] = GlobalConstants.NullName;
                return this.View(transportTypeModel);
            }

            if (transportTypeModel.Any(x => x.Name == transportTypeName))
            {
                this.ViewData[GlobalConstants.Error] = GlobalConstants.TransportTypeNameDuplicate;
                return this.View(transportTypeModel);
            }

            var result = this.adminService.AddTransportType(transportTypeName);

            if (result.Result != GlobalConstants.Success)
            {
                string error = string.Format(GlobalConstants.TransportTypeNameDuplicate);
                this.ViewData[GlobalConstants.Error] = error;
                return this.View(transportTypeModel);
            }

            return this.RedirectToAction("TransportTypes");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTransportType(string id)
        {
            var username = this.User.Identity.Name;

            await this.adminService.DeleteTransportType(id, username);

            return RedirectToAction("TransportTypes");
        }
    }
}
