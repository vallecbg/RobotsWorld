﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GoogleMaps.LocationServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RobotsWorld.Data;
using RobotsWorld.Models;
using RobotsWorld.Services.Constants;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.Deliveries;
using RobotsWorld.ViewModels.OutputModels.Deliveries;

namespace RobotsWorld.Services
{
    public class DeliveryService : BaseService, IDeliveryService
    {
        public DeliveryService(UserManager<User> userManager, RobotsWorldContext context, IMapper mapper) : base(userManager, context, mapper)
        {
        }

        public async Task<string> Create(DeliveryInputModel model)
        {
            var delivery = Mapper.Map<Delivery>(model);
            var sender = this.Context.Users
                .Include(x => x.Robots)
                .First(x => x.Id == model.SenderId);
            var receiver = this.Context.Users
                .Include(x => x.Robots)
                .First(x => x.UserName == model.ReceiverUsername);

            delivery.ReceiverId = receiver.Id;
            delivery.SenderId = sender.Id;
            delivery.SentOn = DateTime.UtcNow;
            
            var robot = this.Context.Robots
                .Include(x => x.Assembly)
                .ThenInclude(x => x.SubAssemblies)
                .ThenInclude(x => x.Parts)
                .First(x => x.Id == model.RobotId);

            robot.UserId = receiver.Id;
            delivery.RobotId = robot.Id;

            sender.Robots.Remove(robot);
            receiver.Robots.Add(robot);

            this.Context.Deliveries.Add(delivery);
            this.Context.Robots.Update(robot);
            this.Context.Users.Update(sender);
            this.Context.Users.Update(receiver);
            await this.Context.SaveChangesAsync();

            return delivery.Id;
        }

        public DeliveryOutputModel GetDeliveryDetails(string id)
        {
            var delivery = this.Context.Deliveries
                .Include(x => x.Robot)
                .Include(x => x.Receiver)
                .Include(x => x.Sender)
                .First(x => x.Id == id);

            var deliveryResult = Mapper.Map<DeliveryOutputModel>(delivery);

            return deliveryResult;
        }
    }
}