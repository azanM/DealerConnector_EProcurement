using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EProcurement.Startup))]
namespace EProcurement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
