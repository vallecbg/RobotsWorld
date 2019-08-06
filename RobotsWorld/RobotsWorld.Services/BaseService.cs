using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RobotsWorld.Data;
using RobotsWorld.Models;

namespace RobotsWorld.Services
{
    public abstract class BaseService
    {
        protected BaseService(UserManager<User> userManager,
            RobotsWorldContext context,
            IMapper mapper)
        {
            this.UserManager = userManager;

            this.Context = context;
            this.Mapper = mapper;
        }

        protected IMapper Mapper { get; }

        protected RobotsWorldContext Context { get; }

        protected UserManager<User> UserManager { get; }
    }
}
