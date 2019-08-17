using System;
using System.Collections.Generic;
using System.Text;
using RobotsWorld.Models;

namespace RobotsWorld.Services.Contracts
{
    public interface ITransportTypeService
    {
        ICollection<string> GetAllTransportTypes();

        TransportType GetTransportTypeByName(string name);
    }
}
