using OnlineMarking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineMarking.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext RContext;
        private DbSet<Record> RecordDB;
        private Record[] Records;
        public StudentController() {
            RContext = new Models.ApplicationDbContext();
            RecordDB = RContext.RecordDB;
            Records = RecordDB.ToArray();
        }
        // GET: Student
        public ViewResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(Record r, HttpPostedFileBase file) {
            if (file == null) {
                return View("../Index");
            }
            var path = Path.Combine(Request.MapPath("~/studentRecord/" + User.Identity.Name), Path.GetFileName(file.FileName));
            try
            {
                if (Directory.Exists(Server.MapPath("~/studentRecord/" + User.Identity.Name)) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~/studentRecord/" + User.Identity.Name));
                }
                file.SaveAs(path);
                r.fileName = file.FileName;
                r.studentName = User.Identity.Name;
                r.filePath = path;  
                RecordDB.Add(r);
                RContext.SaveChanges();
                return View();
            }
            catch {
                return View();
            }
        }
        public ViewResult Result()
        {
            return View(RecordDB.Find(User.Identity.Name));
        }
    }
}
