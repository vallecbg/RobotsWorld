using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RobotsWorld.ViewModels.InputModels.Deliveries;

namespace RobotsWorld.Services.Contracts
{
    public interface IDeliveryService
    {
        Task<string> Create(DeliveryInputModel model);
    }
}
