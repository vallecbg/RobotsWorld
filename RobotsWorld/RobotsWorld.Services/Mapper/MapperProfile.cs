using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using RobotsWorld.Models;
using RobotsWorld.ViewModels.InputModels;
using RobotsWorld.ViewModels.InputModels.Assemblies;
using RobotsWorld.ViewModels.InputModels.Parts;
using RobotsWorld.ViewModels.InputModels.Robots;
using RobotsWorld.ViewModels.InputModels.SubAssemblies;
using RobotsWorld.ViewModels.InputModels.Users;
using RobotsWorld.ViewModels.OutputModels.Robots;
using RobotsWorld.ViewModels.OutputModels.SubAssemblies;

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
                .ForMember(x => x.SubAssemblies, cfg => cfg.MapFrom(x => x.Assembly.SubAssemblies));

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

        }
    }
}
