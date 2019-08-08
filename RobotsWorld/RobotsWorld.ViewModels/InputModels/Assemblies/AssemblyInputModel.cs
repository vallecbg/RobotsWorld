using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RobotsWorld.ViewModels.Constants;

namespace RobotsWorld.ViewModels.InputModels.Assemblies
{
    public class AssemblyInputModel
    {
        [Required]
        [StringLength(ViewModelsConstants.AssemblyNameMaxLength, MinimumLength = ViewModelsConstants.AssemblyNameMinLength)]
        [DataType(DataType.Text)]
        public string Name { get; set; }


        [Required]
        public string RobotId { get; set; }
    }
}
