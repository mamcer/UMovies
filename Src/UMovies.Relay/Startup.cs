using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(UMovies.Relay.Startup))]
namespace UMovies.Relay
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}