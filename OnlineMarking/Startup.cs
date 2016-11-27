using Microsoft.Owin;
using Owin;
using OnlineMarking.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(OnlineMarking.Startup))]
namespace OnlineMarking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Database.SetInitializer(new Initializer.Initializer());
        }
    }
}
