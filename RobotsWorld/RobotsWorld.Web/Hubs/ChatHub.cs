using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RobotsWorld.Data;
using RobotsWorld.Models;

namespace RobotsWorld.Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly RobotsWorldContext db;

        public ChatHub(RobotsWorldContext db)
        {
            this.db = db;
        }
        public async Task SendMessage(string user, string message)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            var chatroomMessage = new ChatRoomMessage
            {
                Username = user,
                PublishedOn = DateTime.Now,
                Content = message
            };

            this.db.ChatRoomMessages.Add(chatroomMessage);
            this.db.SaveChanges();

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
