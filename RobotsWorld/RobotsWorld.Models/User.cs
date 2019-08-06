using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace RobotsWorld.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Robots = new HashSet<Robot>();
        }

        public string Name { get; set; }

        public ICollection<Robot> Robots { get; set; }
    }
}
