using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Practica5_prog3.Startup))]
namespace Practica5_prog3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
