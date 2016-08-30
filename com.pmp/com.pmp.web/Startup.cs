using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(com.pmp.web.Startup))]
namespace com.pmp.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
