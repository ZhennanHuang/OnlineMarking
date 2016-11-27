namespace OnlineMarking.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using OnlineMarking.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineMarking.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
           // AutomaticMigrationDataLossAllowed = true;
            ContextKey = "OnlineMarking.Models.ApplicationDbContext";
        }
        protected override void Seed(OnlineMarking.Models.ApplicationDbContext context)
        {
/*            if (!context.Roles.Any(r=>r.Name=="teacher"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole();
                role.Name = "teacher";
                manager.Create(role);
                context.SaveChanges();
            }
            if (!context.Roles.Any(r => r.Name == "student"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole();
                role.Name = "student";
                manager.Create(role);
                context.SaveChanges();
            }*/
            if (!context.Users.Any(u => u.Email == "studentOne@email.com")) {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var newUser = new ApplicationUser()
                {
                    UserName = "studentOne",
                    Email = "studentOne@email.com",
                    EmailConfirmed = true
                };
                userManager.Create(newUser, "111111`qQ");
                context.Roles.AddOrUpdate(x => x.Name, new IdentityRole { Name = "student" });
                context.SaveChanges();
                userManager.AddToRole(newUser.Id, "student");
                
            }
            if (!context.Users.Any(u => u.Email == "studentTwo@email.com"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var newUser = new ApplicationUser()
                {
                    UserName = "studentTwo",
                    Email = "studentTwo@email.com",
                    EmailConfirmed = true
                };
                userManager.Create(newUser, "222222`qQ");
                userManager.AddToRole(newUser.Id, "student");
                
            }
            if (!context.Users.Any(u => u.Email == "teacher@email.com"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var newUser = new ApplicationUser()
                {
                    UserName = "teacher",
                    Email = "teacher@email.com",
                    EmailConfirmed = true
                };
                userManager.Create(newUser, "333333`qQ");
                context.Roles.AddOrUpdate(x => x.Name, new IdentityRole { Name = "teacher" });
                context.SaveChanges();
                userManager.AddToRole(newUser.Id, "teacher");
            }
            /*context.RecordDB.AddOrUpdate(i => i.filePath,
                new Record {
                    studentName = "studentOne",
                    filePath = "/",
                    marks = "A",
                    feedback = "very good"
                }
                );*/
        }
    }
}
