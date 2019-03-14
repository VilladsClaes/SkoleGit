using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hyponet_TEST.Startup))]
namespace Hyponet_TEST
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
