using gcapi.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace gcapi.Realizations
{
    public class SignalRHub : Hub, ISignalRHub
    {
        public async Task SendMessageToAll(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
