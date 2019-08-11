using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using RobotsWorld.Models;
using RobotsWorld.ViewModels.InputModels;
using RobotsWorld.ViewModels.InputModels.Assemblies;
using RobotsWorld.ViewModels.InputModels.Robots;
using RobotsWorld.ViewModels.OutputModels.Robots;

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

        }
    }
}
