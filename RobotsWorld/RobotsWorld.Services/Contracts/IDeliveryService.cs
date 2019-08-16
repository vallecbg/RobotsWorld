using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RobotsWorld.ViewModels.InputModels.Deliveries;
using RobotsWorld.ViewModels.OutputModels.Deliveries;

namespace RobotsWorld.Services.Contracts
{
    public interface IDeliveryService
    {
        Task<string> Create(DeliveryInputModel model);

        DeliveryOutputModel GetDeliveryDetails(string id);
    }
}
