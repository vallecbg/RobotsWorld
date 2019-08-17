using System;
using System.Collections.Generic;
using System.Text;
using RobotsWorld.Models;
using RobotsWorld.ViewModels.OutputModels.Deliveries;
using RobotsWorld.ViewModels.OutputModels.Robots;

namespace RobotsWorld.ViewModels.OutputModels.Users
{
    public class UserOutputModel
    {
        public UserOutputModel()
        {
            this.Robots = new HashSet<RobotOutputModel>();
            this.SentRobots = new HashSet<DeliveryOutputModel>();
            this.ReceivedRobots = new HashSet<DeliveryOutputModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public ICollection<RobotOutputModel> Robots { get; set; }

        public ICollection<DeliveryOutputModel> SentRobots { get; set; }

        public ICollection<DeliveryOutputModel> ReceivedRobots { get; set; }
    }
}
