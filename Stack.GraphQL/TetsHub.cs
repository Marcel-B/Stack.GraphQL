using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace com.b_velop.stack.GraphQl
{
    public class TetsHub : Hub
    {

        public async Task SendMessage(string user)
        {
            await Clients.All.SendAsync("ReceiveAirData", user);
        }
    }
}
