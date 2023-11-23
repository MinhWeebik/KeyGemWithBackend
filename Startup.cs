using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KeyGem.Startup))]
namespace KeyGem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
