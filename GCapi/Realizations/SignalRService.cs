using gcapi.Dto;
using gcapi.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace gcapi.Realizations
{
    public class SignalRService
    {
        private readonly IHubContext<SignalRHub> _hubContext;

        public SignalRService(IHubContext<SignalRHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNewEventNotification(string eventName, GroupModel theGroup)
        {
            var message = new
            {
                eventName = eventName,
                groupName = theGroup.Name,
                userIds = theGroup.GroupUsers.Select(u => u.Id).ToList()
            };
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", JsonConvert.SerializeObject(message));
        }
    }
}
