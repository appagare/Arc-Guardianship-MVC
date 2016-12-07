using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuardianshipFinancialReporting.Startup))]
namespace GuardianshipFinancialReporting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
