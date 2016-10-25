using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineMarking.Startup))]
namespace OnlineMarking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
