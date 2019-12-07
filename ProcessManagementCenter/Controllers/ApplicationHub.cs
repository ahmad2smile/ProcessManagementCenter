using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcessManagementCenter.Controllers
{
    public class ApplicationHub : Hub
    {
        private readonly List<string> _mineSites;

        public ApplicationHub(IConfiguration configuration)
        {
            _mineSites = configuration.GetSection("AppConfig:MineSites").Get<List<string>>();
        }

        public override async Task OnConnectedAsync()
        {
            foreach (var mineSite in _mineSites)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, mineSite);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            foreach (var mineSite in _mineSites)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, mineSite);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
