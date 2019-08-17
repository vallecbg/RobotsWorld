using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.Models
{
    public class Delivery
    {
        public string Id { get; set; }

        public string SenderId { get; set; }
        public User Sender { get; set; }

        public string ReceiverId { get; set; }
        public User Receiver { get; set; }

        public string RobotId { get; set; }
        public Robot Robot { get; set; }

        public DateTime SentOn { get; set; }

        public string StartingPoint { get; set; }

        public string DestinationPoint { get; set; }

        public decimal Price { get; set; }

        public string TransportTypeId { get; set; }
        public TransportType TransportType { get; set; }
    }
}
