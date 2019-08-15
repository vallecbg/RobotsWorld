using System;
using System.Collections.Generic;

namespace RobotsWorld.Models
{
    public class Robot
    {
        public Robot()
        {
            this.Deliveries = new HashSet<Delivery>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public int Axes { get; set; }

        public string ImageUrl { get; set; }

        public virtual Assembly Assembly { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<Delivery> Deliveries { get; set; }
    }
}
