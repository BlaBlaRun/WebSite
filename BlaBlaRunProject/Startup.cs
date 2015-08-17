using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlaBlaRunProject.WebUI.Startup))]
namespace BlaBlaRunProject.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
