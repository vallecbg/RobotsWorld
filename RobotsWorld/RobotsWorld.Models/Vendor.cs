using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.Models
{
    public class Vendor
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<Part> Parts { get; set; }
    }
}
