using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
        public ActionResult Upload()
        {
            if (User.Identity.IsAuthenticated)
                return View();
            else
                return RedirectToAction("Login","Account");
        }
        [HttpPost]
        public ActionResult Upload(Record r, HttpPostedFileBase file) {

            if (User.Identity.IsAuthenticated)
            {
                if (SorT())
                {
                    if (file == null)
                    {
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
                    catch
                    {
                        return View();
                    }
                }
                else
                    return RedirectToAction("Login", "Account");
            }
            else
                return RedirectToAction("Login","Account");
        }
        public ActionResult Result()
        {
            if (User.Identity.IsAuthenticated)
            {
                Record[] rr;
                if (SorT())
                    rr = RecordDB.FindByName(User.Identity.Name);
                else
                    rr = RecordDB.ToArray();
                return View(rr);
            }
            else
                return RedirectToAction("Login", "Account");
        }
        public Boolean SorT()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var role = UserManager.GetRoles(User.Identity.GetUserId());
                if (role[0] == "student")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
