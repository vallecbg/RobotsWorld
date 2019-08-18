using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RobotsWorld.Services.Constants;
using RobotsWorld.Services.Contracts;

namespace RobotsWorld.Web.Areas.Administration.Controllers
{
    [Area(GlobalConstants.RouteConstants.Administration)]
    [Authorize(Roles = GlobalConstants.Admin)]
    public class AdminsController : Controller
    {
        private readonly IAdminService adminService;

        public AdminsController(IAdminService adminService)
        {
            this.adminService = adminService;
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
    }
}
