using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.ViewModels.OutputModels.Parts
{
    public class PartOutputModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string VendorName { get; set; }
    }
}
