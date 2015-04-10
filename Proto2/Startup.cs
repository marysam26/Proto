using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WriteItUp2.Startup))]
namespace WriteItUp2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
