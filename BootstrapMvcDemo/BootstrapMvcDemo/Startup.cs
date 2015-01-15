using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BootstrapMvcDemo.Startup))]
namespace BootstrapMvcDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
