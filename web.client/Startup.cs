using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(web.client.Startup))]
namespace web.client
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            var configuration = new HubConfiguration { EnableDetailedErrors = true };
            app.MapSignalR("/signalr", configuration);
        }
    }
}