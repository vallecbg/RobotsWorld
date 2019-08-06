using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RobotsWorld.ViewModels.InputModels.Robots;

namespace RobotsWorld.Services.Contracts
{
    public interface IRobotService
    {
        Task<string> CreateRobot(RobotInputModel model);
    }
}
