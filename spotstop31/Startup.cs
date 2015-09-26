using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(spotstop31.Startup))]
namespace spotstop31
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
