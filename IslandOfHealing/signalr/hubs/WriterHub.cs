using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IslandOfHealing.signalr.hubs
{
    public class WriterHub : Hub
    {
        public void SendNotification(string name, string notification)
        {
            Clients.All.addNewMessageToPage(name, notification);
        }
    }
}