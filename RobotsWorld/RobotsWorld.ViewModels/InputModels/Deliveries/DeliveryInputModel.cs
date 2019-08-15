using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RobotsWorld.ViewModels.Constants;

namespace RobotsWorld.ViewModels.InputModels.Deliveries
{
    public class DeliveryInputModel
    {
        [Required]
        [StringLength(ViewModelsConstants.DeliveryStartPointMaxLength, MinimumLength = ViewModelsConstants.DeliveryStartPointMinLength)]
        public string StartingPoint { get; set; }

        [Required]
        [StringLength(ViewModelsConstants.DeliveryDestinationPointMaxLength, MinimumLength = ViewModelsConstants.DeliveryDestinationPointMinLength)]
        public string DestinationPoint { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        [StringLength(ViewModelsConstants.DeliveryReceiverUsernameMaxLength, MinimumLength = ViewModelsConstants.DeliveryReceiverUsernameMinLength)]
        public string ReceiverUsername { get; set; }

        [Required]
        public string RobotId { get; set; }

        [Required]
        [Range(ViewModelsConstants.DeliveryMinPrice, ViewModelsConstants.DeliveryMaxPrice)]
        public decimal Price { get; set; }
    }
}
