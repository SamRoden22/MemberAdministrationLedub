using Microsoft.AspNetCore.SignalR;

namespace MemberAdministrationLedub
{
    public class HubClass : Hub
    {
        public async Task SendMessage(string message)
        {
            // Broadcast the message to all connected clients
            await Clients.All.SendAsync("MemberDeleted", message);
        }

        public async Task DeleteMember(string message)
        {
            // Broadcast the message to all connected clients
            await Clients.All.SendAsync("MemberDeleted", message);
        }
    }
}
