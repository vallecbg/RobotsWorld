using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.ViewModels.OutputModels.Chatrooms
{
    public class ChatroomMessageOutputModel
    {
        public string Username { get; set; }

        public DateTime PublishedOn { get; set; }

        public string Content { get; set; }
    }
}
