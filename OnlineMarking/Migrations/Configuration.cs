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
            ContextKey = "OnlineMarking.Models.ApplicationDbContext";
        }

        protected override void Seed(OnlineMarking.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("teacher"))
            {
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
            if (!context.Users.Any(u => u.Email == "studentOne@email.com")) {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var newUser = new ApplicationUser()
                {
                    UserName = "studentOne",
                    Email = "studentOne@email.com",
                };
                userManager.Create(newUser, "111111`qQ");
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
                };
                userManager.Create(newUser, "333333`qQ");
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
