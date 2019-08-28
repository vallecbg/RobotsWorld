using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.Models
{
    public class ChatRoomMessage
    {
        public string Id { get; set; }

        public DateTime PublishedOn { get; set; }

        public string Username { get; set; }

        public string Content { get; set; }
    }
}
