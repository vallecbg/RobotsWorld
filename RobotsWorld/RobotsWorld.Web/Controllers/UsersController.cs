using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RobotsWorld.Services.Constants;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels;
using RobotsWorld.ViewModels.InputModels.Users;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace RobotsWorld.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterInputModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(registerModel);
            }
            var result = this.userService.Register(registerModel).Result;
            if (result != SignInResult.Success)
            {
                this.ViewData[GlobalConstants.ModelError] = string.Format(GlobalConstants.RegisterError);
                return this.View(registerModel);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginInputModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(loginModel);
            }

            var result = this.userService.Login(loginModel);

            if (result != SignInResult.Success)
            {
                this.ViewData[GlobalConstants.ModelError] = GlobalConstants.LoginError;
                return this.View(loginModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            this.userService.Logout();

            return RedirectToAction("Index", "Home");
        }
    }
}
