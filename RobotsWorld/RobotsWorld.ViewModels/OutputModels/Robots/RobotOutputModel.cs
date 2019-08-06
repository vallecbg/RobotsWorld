using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.ViewModels.OutputModels.Robots
{
    public class RobotOutputModel
    {
        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public int Axes { get; set; }

        public string ImageUrl { get; set; }

        public int AssembliesCount { get; set; }

        public string User { get; set; }
    }
}
