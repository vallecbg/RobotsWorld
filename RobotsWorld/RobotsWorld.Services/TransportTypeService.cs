using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RobotsWorld.Data;
using RobotsWorld.Models;
using RobotsWorld.Services.Contracts;

namespace RobotsWorld.Services
{
    public class TransportTypeService : BaseService, ITransportTypeService
    {
        public TransportTypeService(UserManager<User> userManager, RobotsWorldContext context, IMapper mapper) : base(userManager, context, mapper)
        {
        }

        public ICollection<string> GetAllTransportTypes()
        {
            var transportTypes = this.Context.TransportTypes
                .Select(x => x.Name)
                .ToList();

            return transportTypes;
        }

        public TransportType GetTransportTypeByName(string name)
        {
            var transportType = this.Context.TransportTypes
                .Include(x => x.Deliveries)
                .First(x => x.Name == name);

            return transportType;
        }
    }
}
