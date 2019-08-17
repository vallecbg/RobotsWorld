using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.Models
{
    public class TransportType
    {
        public TransportType()
        {
            this.Deliveries = new HashSet<Delivery>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<Delivery> Deliveries { get; set; }
    }
}
