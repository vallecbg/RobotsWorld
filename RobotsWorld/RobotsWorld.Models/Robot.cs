using System;

namespace RobotsWorld.Models
{
    public class Robot
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public int Axes { get; set; }

        public string ImageUrl { get; set; }

        public string AssemblyId { get; set; }
        public Assembly Assembly { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
