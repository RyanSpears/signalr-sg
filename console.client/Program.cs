using System;
using Microsoft.AspNet.SignalR.Client;

namespace console.client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CONSOLE CLIENT");
            Console.WriteLine("------");

            // Setup connection to hub
            var firstHubConnection = new HubConnection("http://localhost:6024");

            // Connection state changed notification
            firstHubConnection.StateChanged += stateChange =>
                Console.WriteLine("SignalR {0} >> {1} ({2})",
                stateChange.OldState,
                stateChange.NewState,
                firstHubConnection.Transport == null ? "<<null>>" : firstHubConnection.Transport.Name);

            var firstHubProxy = firstHubConnection.CreateHubProxy("FirstHub");

            // Subscribe to incoming server messages
            firstHubProxy.On<string>("serverMessage", message => Console.WriteLine("Server Message: {0}", message));
            firstHubProxy.On<string>("clientMessageFromServer", message => Console.WriteLine("Client Message From Server: {0}", message));

            firstHubConnection.Start();

            while (true)
            {
                var input = Console.ReadLine();

                // Want to call back to the server
                firstHubProxy.Invoke("HubReceiveMessage", input);
            }
        }
    }
}
