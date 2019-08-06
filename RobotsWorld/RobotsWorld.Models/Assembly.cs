using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace RobotsWorld.Models
{
    public class Assembly
    {
        public Assembly()
        {
            this.Robots = new HashSet<Robot>();
            this.SubAssemblies = new HashSet<SubAssembly>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public decimal TotalPrice => this.SubAssemblies.Any() ? this.SubAssemblies.Sum(x => x.Price * x.Quantity) : 0;

        public ICollection<Robot> Robots { get; set; }

        public ICollection<SubAssembly> SubAssemblies { get; set; }
    }
}
