using System;

namespace RobotsWorld.Models
{
    public class Robot
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public int Axis { get; set; }

        public string ImageUrl { get; set; }

        public string AssemblyId { get; set; }
        public Assembly Assembly { get; set; }
    }
}
