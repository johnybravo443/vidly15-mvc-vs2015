using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vidly15.Startup))]
namespace Vidly15
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
