using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalRChat.Web.Startup))]

namespace SignalRChat.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
