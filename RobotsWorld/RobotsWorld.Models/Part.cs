using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.Models
{
    public class Part
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string SubAssemblyId { get; set; }
        public SubAssembly SubAssembly { get; set; }

        public string VendorId { get; set; }
        public Vendor Vendor { get; set; }
    }
}
