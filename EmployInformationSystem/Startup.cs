using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmployInformationSystem.Startup))]
namespace EmployInformationSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
