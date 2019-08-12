using System;
using System.Collections.Generic;
using System.Text;
using RobotsWorld.Models;
using RobotsWorld.ViewModels.OutputModels.SubAssemblies;

namespace RobotsWorld.ViewModels.OutputModels.Robots
{
    public class RobotOutputModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public int Axes { get; set; }

        public string ImageUrl { get; set; }

        public int AssembliesCount { get; set; }

        public string User { get; set; }

        public string AssemblyId { get; set; }

        public decimal TotalPrice { get; set; }

        public double TotalWeight { get; set; }

        public ICollection<SubAssemblyOutputModel> SubAssemblies { get; set; }
    }
}
