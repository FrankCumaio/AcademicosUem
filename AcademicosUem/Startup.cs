using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AcademicosUem.Startup))]
namespace AcademicosUem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
