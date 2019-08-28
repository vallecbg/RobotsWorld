using System;
using System.Collections.Generic;
using System.Text;
using RobotsWorld.ViewModels.OutputModels.Parts;

namespace RobotsWorld.ViewModels.OutputModels.SubAssemblies
{
    public class SubAssemblyDetailsOutputModel
    {
        public SubAssemblyDetailsOutputModel()
        {
            this.Parts = new HashSet<PartOutputModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Weight { get; set; }

        public decimal TotalPrice { get; set; }

        public int PartsCount { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<PartOutputModel> Parts { get; set; }
    }
}
