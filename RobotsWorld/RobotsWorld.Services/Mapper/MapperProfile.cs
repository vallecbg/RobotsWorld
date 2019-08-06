using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using RobotsWorld.Models;
using RobotsWorld.ViewModels.InputModels;

namespace RobotsWorld.Services.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RegisterInputModel, User>();
        }
    }
}
