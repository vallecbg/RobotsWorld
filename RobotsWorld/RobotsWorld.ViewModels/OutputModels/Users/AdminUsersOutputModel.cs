using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.ViewModels.OutputModels.Users
{
    public class AdminUsersOutputModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }

        public int RobotsCount { get; set; }

        public int SentRobotsCount { get; set; }

        public int ReceivedRobotsCount { get; set; }
    }
}
