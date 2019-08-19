using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using RobotsWorld.Models;
using RobotsWorld.ViewModels.InputModels;
using RobotsWorld.ViewModels.InputModels.Assemblies;
using RobotsWorld.ViewModels.InputModels.Deliveries;
using RobotsWorld.ViewModels.InputModels.Parts;
using RobotsWorld.ViewModels.InputModels.Robots;
using RobotsWorld.ViewModels.InputModels.SubAssemblies;
using RobotsWorld.ViewModels.InputModels.Users;
using RobotsWorld.ViewModels.OutputModels.Deliveries;
using RobotsWorld.ViewModels.OutputModels.Robots;
using RobotsWorld.ViewModels.OutputModels.SubAssemblies;
using RobotsWorld.ViewModels.OutputModels.TransportTypes;
using RobotsWorld.ViewModels.OutputModels.Users;
using RobotsWorld.ViewModels.OutputModels.Vendors;

namespace RobotsWorld.Services.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RegisterInputModel, User>();

            CreateMap<RobotInputModel, Robot>()
                .ForMember(x => x.Name, cfg => cfg.MapFrom(x => x.Name))
                .ForMember(x => x.Axes, cfg => cfg.MapFrom(x => x.Axes))
                .ForMember(x => x.SerialNumber, cfg => cfg.MapFrom(x => x.SerialNumber))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Robot, RobotOutputModel>()
                .ForMember(x => x.User, cfg => cfg.MapFrom(x => x.User.UserName))
                .ForMember(x => x.AssembliesCount, cfg => cfg.MapFrom(x => x.Assembly.SubAssemblies.Count))
                .ForMember(x => x.AssemblyId, cfg => cfg.MapFrom(x => x.Assembly.Id))
                .ForMember(x => x.SubAssemblies, cfg => cfg.MapFrom(x => x.Assembly.SubAssemblies))
                .ForMember(x => x.TotalPrice, cfg => cfg.MapFrom(x => x.Assembly.TotalPrice))
                .ForMember(x => x.TotalWeight, cfg => cfg.MapFrom(x => x.Assembly.TotalWeight));

            CreateMap<SubAssemblyInputModel, SubAssembly>()
                .ForMember(x => x.Name, cfg => cfg.MapFrom(x => x.Name))
                .ForMember(x => x.Quantity, cfg => cfg.MapFrom(x => x.Quantity))
                .ForMember(x => x.Weight, cfg => cfg.MapFrom(x => x.Weight))
                .ForMember(x => x.AssemblyId, cfg => cfg.MapFrom(x => x.AssemblyId))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<PartInputModel, Part>()
                .ForMember(x => x.Name, cfg => cfg.MapFrom(x => x.Name))
                .ForMember(x => x.Quantity, cfg => cfg.MapFrom(x => x.Quantity))
                .ForMember(x => x.Price, cfg => cfg.MapFrom(x => x.Price));

            CreateMap<SubAssembly, SubAssemblyOutputModel>()
                .ForMember(x => x.TotalPrice, cfg => cfg.MapFrom(x => x.PartsPrice));

            CreateMap<DeliveryInputModel, Delivery>()
                .ForMember(x => x.StartingPoint, cfg => cfg.MapFrom(x => x.StartingPoint))
                .ForMember(x => x.DestinationPoint, cfg => cfg.MapFrom(x => x.DestinationPoint))
                .ForMember(x => x.Price, cfg => cfg.MapFrom(x => x.Price))
                .ForMember(x => x.RobotId, cfg => cfg.MapFrom(x => x.RobotId))
                .ForMember(x => x.SenderId, cfg => cfg.MapFrom(x => x.SenderId))
                .ForMember(x => x.SentOn, cfg => cfg.MapFrom(x => DateTime.UtcNow))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Delivery, DeliveryOutputModel>()
                .ForMember(x => x.Id, cfg => cfg.MapFrom(x => x.Id))
                .ForMember(x => x.DestinationPoint, cfg => cfg.MapFrom(x => x.DestinationPoint))
                .ForMember(x => x.StartingPoint, cfg => cfg.MapFrom(x => x.StartingPoint))
                .ForMember(x => x.Price, cfg => cfg.MapFrom(x => x.Price))
                .ForMember(x => x.ReceiverName, cfg => cfg.MapFrom(x => x.Receiver.UserName))
                .ForMember(x => x.SenderName, cfg => cfg.MapFrom(x => x.Sender.UserName))
                .ForMember(x => x.RobotName, cfg => cfg.MapFrom(x => x.Robot.Name))
                .ForMember(x => x.SentOn, cfg => cfg.MapFrom(x => x.SentOn))
                .ForMember(x => x.TransportTypeName, cfg => cfg.MapFrom(x => x.TransportType.Name));

            CreateMap<User, UserOutputModel>()
                .ForMember(x => x.Id, cfg => cfg.MapFrom(x => x.Id))
                .ForMember(x => x.Username, cfg => cfg.MapFrom(x => x.UserName))
                .ForMember(x => x.Name, cfg => cfg.MapFrom(x => x.Name))
                .ForMember(x => x.Robots, cfg => cfg.MapFrom(x => x.Robots))
                .ForMember(x => x.ReceivedRobots, cfg => cfg.MapFrom(x => x.ReceivedRobots))
                .ForMember(x => x.SentRobots, cfg => cfg.MapFrom(x => x.SentRobots));

            CreateMap<User, AdminUsersOutputModel>()
                .ForMember(x => x.RobotsCount, cfg => cfg.MapFrom(x => x.Robots.Count))
                .ForMember(x => x.SentRobotsCount, cfg => cfg.MapFrom(x => x.SentRobots.Count))
                .ForMember(x => x.ReceivedRobotsCount, cfg => cfg.MapFrom(x => x.ReceivedRobots.Count));

            CreateMap<User, ChangingRoleModel>()
                .ForMember(x => x.Id, cfg => cfg.MapFrom(x => x.Id))
                .ForMember(x => x.Name, cfg => cfg.MapFrom(x => x.Name))
                .ForMember(x => x.Role, cfg => cfg.Ignore())
                .ForMember(x => x.NewRole, cfg => cfg.Ignore())
                .ForMember(x => x.AppRoles, cfg => cfg.Ignore());

            CreateMap<Vendor, VendorAdminOutputModel>()
                .ForMember(x => x.PartsCount, cfg => cfg.MapFrom(x => x.Parts.Count));

            CreateMap<Robot, RobotAdminOutputModel>()
                .ForMember(x => x.SubAssembliesCount, cfg => cfg.MapFrom(x => x.Assembly.SubAssemblies.Count))
                .ForMember(x => x.DeliveriesCount, cfg => cfg.MapFrom(x => x.Deliveries.Count))
                .ForMember(x => x.User, cfg => cfg.MapFrom(x => x.User.UserName))
                .ForMember(x => x.TotalPrice, cfg => cfg.MapFrom(x => x.Assembly.TotalPrice))
                .ForMember(x => x.TotalWeight, cfg => cfg.MapFrom(x => x.Assembly.TotalWeight));

            CreateMap<TransportType, TransportTypeOutputModel>()
                .ForMember(x => x.DeliveriesCount, cfg => cfg.MapFrom(x => x.Deliveries.Count))
                .ForMember(x => x.Name, cfg => cfg.MapFrom(x => x.Name));

            CreateMap<SubAssembly, SubAssemblyDetailsOutputModel>()
                .ForMember(x => x.ImageUrl,cfg => cfg.MapFrom(x => x.ImageUrl))
                .ForMember(x => x.PartsCount, cfg => cfg.MapFrom(x => x.Parts.Count));
        }
    }
}
