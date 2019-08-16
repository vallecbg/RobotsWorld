using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.ViewModels.OutputModels.Deliveries
{
    public class DeliveryOutputModel
    {
        public string Id { get; set; }

        public string SenderName { get; set; }

        public string ReceiverName { get; set; }

        public string RobotName { get; set; }

        public DateTime SentOn { get; set; }

        public string StartingPoint { get; set; }

        public string DestinationPoint { get; set; }

        public decimal Price { get; set; }

        //TODO: Add the transport type
    }
}
