using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RobotsWorld.Services.Constants;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.Deliveries;

namespace RobotsWorld.Web.Controllers
{
    [Authorize]
    public class DeliveryController : Controller
    {
        private readonly IDeliveryService deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            this.deliveryService = deliveryService;
        }

        [HttpGet]
        [Route(GlobalConstants.RouteConstants.CreateDeliveryRoute)]
        public IActionResult Create(string robotId)
        {
            this.ViewData[GlobalConstants.RobotId] = robotId;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DeliveryInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var robotId = this.deliveryService.Create(model).Result;

            return RedirectToAction("Details", "Delivery", new{id = robotId});
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var delivery = this.deliveryService.GetDeliveryDetails(id);

            return this.View(delivery);
        } 
    }
}
