using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace RobotsWorld.Models
{
    public class Assembly
    {
        /*
         * An ASSEMBLY is a combination of two or more sub assemblies joined to perform a specific function.
         * A SUB ASSEMBLY consists of two or more parts that form a portion of an assembly.
         * It can be replaced as a whole, but some of its parts can be replaced individually.
         */
        public Assembly()
        {
            this.SubAssemblies = new HashSet<SubAssembly>();
        }

        public string Id { get; set; }

        //public string Name { get; set; }

        //TODO: Check it
        public decimal TotalPrice => this.SubAssemblies.Any() ? this.SubAssemblies.Sum(x => x.PartsPrice * x.Quantity) + this.Robot.Deliveries.Sum(x => x.Price) : 0;

        public double TotalWeight => this.SubAssemblies.Any() ? this.SubAssemblies.Sum(x => x.Weight * x.Quantity) : 0;

        public string RobotId { get; set; }
        public Robot Robot { get; set; }

        public ICollection<SubAssembly> SubAssemblies { get; set; }
    }
}
