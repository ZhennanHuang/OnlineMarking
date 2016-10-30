using Microsoft.Owin;
using Owin;
using OnlineMarking.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

[assembly: OwinStartupAttribute(typeof(OnlineMarking.Startup))]
namespace OnlineMarking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRoles();
        }
        private void createRoles() {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("teacher")) {
                var role = new IdentityRole();
                role.Name = "teacher";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("student"))
            {
                var role = new IdentityRole();
                role.Name = "student";
                roleManager.Create(role);
            }
        }
    }
}
