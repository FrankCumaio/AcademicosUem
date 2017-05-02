using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SCMA.Startup))]
namespace SCMA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
