using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using RobotsWorld.ViewModels.Constants;

namespace RobotsWorld.ViewModels.InputModels.SubAssemblies
{
    public class SubAssemblyInputModel
    {
        [Required]
        [Display(Name = ViewModelsConstants.Name)]
        [StringLength(ViewModelsConstants.SubAssemblyNameMaxLength, MinimumLength = ViewModelsConstants.SubAssemblyNameMinLength)]
        public string Name { get; set; }

        [Required]
        [Display(Name = ViewModelsConstants.Quantity)]
        [Range(ViewModelsConstants.SubAssemblyMinQuantity, ViewModelsConstants.SubAssemblyMaxQuantity)]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = ViewModelsConstants.Weight)]
        [Range(ViewModelsConstants.SubAssemblyMinWeight, ViewModelsConstants.SubAssemblyMaxWeight)]
        public double Weight { get; set; }

        [Required]
        public string AssemblyId { get; set; }

        [Display(Name = ViewModelsConstants.ImageDisplay)]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
    }
}
