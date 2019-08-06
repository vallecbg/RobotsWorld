﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using RobotsWorld.ViewModels.Constants;

namespace RobotsWorld.ViewModels.InputModels.Robots
{
    public class RobotInputModel
    {
        [Required]
        [StringLength(ViewModelsConstants.RobotNameMaxLength, MinimumLength = ViewModelsConstants.RobotNameMinLength)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(ViewModelsConstants.SerialNumberLength)]
        public string SerialNumber { get; set; }

        [Required]
        public int Axes { get; set; }

        [Required]
        public string User { get; set; }

        [Display(Name = ViewModelsConstants.ImageDisplay)]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
    }
}
