using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.ViewModels.OutputModels.SubAssemblies
{
    public class SubAssemblyDetailsOutputModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Weight { get; set; }

        public decimal TotalPrice { get; set; }

        public int PartsCount { get; set; }

        public string ImageUrl { get; set; }
    }
}
