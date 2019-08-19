using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.ViewModels.OutputModels.Robots
{
    public class RobotAdminOutputModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public int Axes { get; set; }

        public string User { get; set; }

        public decimal TotalPrice { get; set; }

        public double TotalWeight { get; set; }

        public int SubAssembliesCount { get; set; }

        public int DeliveriesCount { get; set; }
    }
}
