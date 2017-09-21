using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LuomuTila.Startup))]
namespace LuomuTila
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
