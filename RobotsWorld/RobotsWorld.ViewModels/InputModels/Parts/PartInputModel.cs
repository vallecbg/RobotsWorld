using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RobotsWorld.ViewModels.Constants;

namespace RobotsWorld.ViewModels.InputModels.Parts
{
    public class PartInputModel
    {
        [Required]
        [StringLength(ViewModelsConstants.PartNameMaxLength, MinimumLength = ViewModelsConstants.PartNameMinLength)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        public string VendorName { get; set; }

        [Required]
        [Range(ViewModelsConstants.PartMinQuantity, ViewModelsConstants.PartMaxQuantity)]
        public int Quantity { get; set; }

        [Required]
        [Range(ViewModelsConstants.PartMinPrice, ViewModelsConstants.PartMaxPrice)]
        public decimal Price { get; set; }

        [Required]
        public string SubAssemblyId { get; set; }
    }
}
