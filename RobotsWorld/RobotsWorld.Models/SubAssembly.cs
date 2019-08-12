﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsWorld.Models
{
    public class SubAssembly
    {
        public SubAssembly()
        {
            this.Parts = new HashSet<Part>();
        }

        public string Id { get; set; }

        public string AssemblyId { get; set; }
        public Assembly Assembly { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal PartsPrice
        {
            get
            {
                if (this.Parts.Any())
                {
                    return this.Parts.Sum(x => x.Price * x.Quantity);
                }
                else
                {
                    return 0;
                }
            }
        }

        public double Weight { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Part> Parts { get; set; }
    }
}
