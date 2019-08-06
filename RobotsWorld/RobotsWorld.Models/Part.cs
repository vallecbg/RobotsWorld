﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.Models
{
    public class Part
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string SubAssemblyId { get; set; }
        public SubAssembly SubAssembly { get; set; }
    }
}
