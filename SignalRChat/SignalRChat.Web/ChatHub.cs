using Microsoft.AspNet.SignalR;

namespace SignalRChat.Web
{
    public class ChatHub : Hub
    {
        public void Send(string name, int color, string message)
        {
            Clients.All.broadcastMessage(name, color, message);
        }
    }
}