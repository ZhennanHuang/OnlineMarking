using OnlineMarking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace OnlineMarking.Initializer
{
    internal class Initializer: MigrateDatabaseToLatestVersion<ApplicationDbContext,Migrations.Configuration>
    {
        
    }
}