using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Enlighten.Startup))]
namespace Enlighten
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
