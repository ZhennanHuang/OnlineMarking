using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineMarking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineMarking.Controllers
{
    public class TeacherController : Controller
    {
        private ApplicationDbContext RContext;
        private DbSet<Record> RecordDB;
        private Record[] Records;
        public TeacherController()
        {
            RContext = new Models.ApplicationDbContext();
            RecordDB = RContext.RecordDB;
            Records = RecordDB.ToArray();
        }
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RecordList() {              //view all the record that uploaded by students
        
            if (User.Identity.IsAuthenticated)
            {
                Record[] rr;
                if (SorT())
                    return RedirectToAction("Index", "Home");
                else
                    rr = RecordDB.ToArray();
                return View(rr);
            }
            else
                return RedirectToAction("Login", "Account");
        }



        public ActionResult Mark()      //the record detail and teachers are able to mark in this view
        {
            Record Rmark = new Record();
            Rmark.marks = "A";
            Generatemarks();
            return View(Rmark);
        }
        private void Generatemarks()
        {
            ViewData["marks"] = GetMarksList();
        }

        private List<SelectListItem> GetMarksList()
        {
            List<SelectListItem> MarksList = new List<SelectListItem>();
            
            //this is give student marks（A/B/C/D/F）
           
            throw new NotImplementedException();
        }

        public Boolean SorT()           //make sure the user is student or teacher
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var role = UserManager.GetRoles(User.Identity.GetUserId());
                if (role[0] == "teacher")
                {
                    return true;
                }
            }
            return false;
        }
    }
}