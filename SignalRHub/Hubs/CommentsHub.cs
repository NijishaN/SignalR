using Microsoft.AspNetCore.SignalR;

namespace SignalRHub.Hubs
{
    public class CommentsHub : Hub
    {
        public async Task SendMessage(string name, string message)
        {
            await Clients.All.SendAsync("ReceivedComment", name, message);
        }
    }
}
