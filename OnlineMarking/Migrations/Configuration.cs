namespace OnlineMarking.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using OnlineMarking.Models;

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
            context.RecordDB.AddOrUpdate(i => i.filePath,
                new Record {
                    studentName = "student one",
                    filePath = "/",
                    marks = 10
                }
                );
        }
    }
}
