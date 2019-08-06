using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.Models
{
    public class SubAssembly
    {
        public string Id { get; set; }

        public string AssemblyId { get; set; }
        public Assembly Assembly { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Part> Parts { get; set; }
    }
}
