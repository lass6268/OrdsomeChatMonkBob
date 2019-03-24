using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

namespace SignalRMVCChat
{
    public class ChatHub : Hub
    {
        private readonly IChatContext _context;
        public ChatHub(IChatContext context)
        {
            _context = context;
        }

        public async Task SendMessage(Guid room, string message)
        {
            var i = Context.ConnectionId;
            var ChatMessage = new Models.ChatElement
            {
                User = i,
                Time = DateTimeOffset.UtcNow,
                Message = message
            };
            await _context.AddMessage(room, ChatMessage);
            await Clients.Group(room.ToString()).SendAsync("ReceiveMessage",ChatMessage.User,ChatMessage.Time,ChatMessage.Message);
        }

        public void JoinRoom(Guid room)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, room.ToString());

        }
        public async Task LoadHistory(Guid roomId)
        {
            var history = await _context.GetMessageHistory(roomId);

            await Clients.Caller.SendAsync("ReceiveMessages", history);

        }
        public async Task CorrectAnswer(Guid room,string user,string message)
        {

            await Clients.Group(room.ToString()).SendAsync("AlertCorrectIsFound", user,message);
        }
    }
}
