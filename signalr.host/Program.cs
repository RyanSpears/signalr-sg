using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;

namespace signalr.host
{
    class Program
    {
        static void Main(string[] args)
        {
            const string uri = "http://localhost:6024";

            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine("SERVER");
                Console.WriteLine("------");
                Console.WriteLine("Server started on {0}.", uri);

                var hub = GlobalHost.ConnectionManager.GetHubContext<FirstHub>();

                while (true)
                {
                    var input = Console.ReadLine();

                    hub.Clients.All.serverMessage(String.Format("{0} says {1}", Environment.UserName, input));
                }
            }
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            var configuration = new HubConfiguration { EnableDetailedErrors = true };
            app.MapSignalR("/signalr", configuration);
        }
    }

    [HubName("firstHub")]
    public class FirstHub : Hub
    {
        public void HubReceiveMessage(string message)
        {
            Console.WriteLine("FirstHub.ReceiveMessage(): {0}", message);
            Clients.All.clientMessageFromServer(message);
        }
    }
}
