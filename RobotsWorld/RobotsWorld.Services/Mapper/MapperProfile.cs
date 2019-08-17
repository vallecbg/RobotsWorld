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
using RobotsWorld.ViewModels.OutputModels.Users;

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
                .ForMember(x => x.SentOn, cfg => cfg.MapFrom(x => x.SentOn));

            CreateMap<User, UserOutputModel>()
                .ForMember(x => x.Id, cfg => cfg.MapFrom(x => x.Id))
                .ForMember(x => x.Username, cfg => cfg.MapFrom(x => x.UserName))
                .ForMember(x => x.Name, cfg => cfg.MapFrom(x => x.Name))
                .ForMember(x => x.Robots, cfg => cfg.MapFrom(x => x.Robots))
                .ForMember(x => x.ReceivedRobots, cfg => cfg.MapFrom(x => x.ReceivedRobots))
                .ForMember(x => x.SentRobots, cfg => cfg.MapFrom(x => x.SentRobots));
        }
    }
}
