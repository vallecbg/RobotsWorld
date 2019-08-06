using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.Models
{
    public class Assembly
    {
        public string Id { get; set; }

        public ICollection<SubAssembly> SubAssemblies { get; set; }
    }
}
