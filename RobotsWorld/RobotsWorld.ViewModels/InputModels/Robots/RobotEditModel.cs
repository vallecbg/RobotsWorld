using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using RobotsWorld.ViewModels.Constants;

namespace RobotsWorld.ViewModels.InputModels.Robots
{
    public class RobotEditModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(ViewModelsConstants.RobotNameMaxLength, MinimumLength = ViewModelsConstants.RobotNameMinLength)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [StringLength(ViewModelsConstants.SerialNumberMaxLength, MinimumLength = ViewModelsConstants.SerialNumberMinLength)]
        public string SerialNumber { get; set; }

        [Required]
        public int Axes { get; set; }

        [Display(Name = ViewModelsConstants.ImageDisplay)]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
    }
}
