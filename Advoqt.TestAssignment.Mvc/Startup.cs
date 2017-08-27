using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Advoqt.TestAssignment.Mvc.Startup))]
namespace Advoqt.TestAssignment.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
