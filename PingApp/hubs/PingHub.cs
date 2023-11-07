using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace PingHubNameSpace;
public class PingHub : Hub
{
    public async Task SendPing()
    {
        while (true)
        {
            await Clients.Caller.SendAsync("ReceivePing", "Ping");
            await Task.Delay(TimeSpan.FromSeconds(30));
        }
    }
}
