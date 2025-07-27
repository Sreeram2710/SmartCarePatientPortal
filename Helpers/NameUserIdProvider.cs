using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace SmartCarePatientPortal.Helpers
{
    public class NameUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            // Will use User.Identity.Name for message routing
            return connection.User?.Identity?.Name;
        }
    }
}
