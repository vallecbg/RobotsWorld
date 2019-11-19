using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RobotsWorld.Models;
using RobotsWorld.ViewModels.InputModels.Robots;
using RobotsWorld.ViewModels.OutputModels.Robots;

namespace RobotsWorld.Services.Contracts
{
    public interface IRobotService
    {
        Task<string> CreateRobot(RobotInputModel model);

        ICollection<RobotOutputModel> GetUserRobots(string userId);

        RobotOutputModel GetRobotDetails(string robotId);

        void DeleteRobot(string robotId, string username);

        void EditRobot(RobotEditModel model);

        void DeleteAllRobotRelations(Robot robot);

        RobotEditModel GetRobotToEdit(string robotId);

        int GetAssembliesCount(string id);

        bool CheckUserIsOwnerOfRobot(string userId, string robotId);
    }
}
