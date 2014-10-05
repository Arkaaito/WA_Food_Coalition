using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GiveFoodNow.Startup))]
namespace GiveFoodNow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
