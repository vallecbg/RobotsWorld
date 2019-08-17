using System;
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
        private readonly ITransportTypeService transportTypeService;

        public DeliveryService(UserManager<User> userManager, RobotsWorldContext context, IMapper mapper, ITransportTypeService transportTypeService)
            : base(userManager, context, mapper)
        {
            this.transportTypeService = transportTypeService;
        }

        public async Task<string> Create(DeliveryInputModel model)
        {
            var delivery = Mapper.Map<Delivery>(model);
            var sender = this.Context.Users
                .Include(x => x.Robots)
                .Include(x => x.SentRobots)
                .Include(x => x.ReceivedRobots)
                .First(x => x.Id == model.SenderId);
            var receiver = this.Context.Users
                .Include(x => x.Robots)
                .Include(x => x.SentRobots)
                .Include(x => x.ReceivedRobots)
                .First(x => x.UserName == model.ReceiverUsername);

            delivery.ReceiverId = receiver.Id;

            var transportType = this.transportTypeService.GetTransportTypeByName(model.TransportTypeName);
            delivery.TransportTypeId = transportType.Id;
            
            var robot = this.Context.Robots
                .Include(x => x.Deliveries)
                .Include(x => x.Assembly)
                .ThenInclude(x => x.SubAssemblies)
                .ThenInclude(x => x.Parts)
                .First(x => x.Id == model.RobotId);

            robot.UserId = receiver.Id;
            robot.Deliveries.Add(delivery);

            sender.Robots.Remove(robot);
            receiver.Robots.Add(robot);
            
            sender.SentRobots.Add(delivery);
            receiver.ReceivedRobots.Add(delivery);

            this.Context.Deliveries.Update(delivery);
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
                .Include(x => x.TransportType)
                .Include(x => x.Receiver)
                .Include(x => x.Sender)
                .First(x => x.Id == id);

            var deliveryResult = Mapper.Map<DeliveryOutputModel>(delivery);

            return deliveryResult;
        }
    }
}
