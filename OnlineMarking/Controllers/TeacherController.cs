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
                if (!SorT())
                    return RedirectToAction("Index", "Home");
                rr = RecordDB.ToArray();
                return View(rr);
            }
            else
                return RedirectToAction("Login", "Account");
        }


        
        public ActionResult Mark()      //the record detail and teachers are able to mark in this view
        {
            //List<string> marks=new List<string>() { "A", "B", "C", "D", "F" };
            var marks = new List<SelectListItem>()
            {
                (new SelectListItem() {Text = "A", Value = "A", Selected = false}),
                (new SelectListItem() {Text = "B", Value = "B", Selected = false}),
                (new SelectListItem() {Text = "C", Value = "C", Selected = false}),
                (new SelectListItem() {Text = "D", Value = "D", Selected = false}),
                (new SelectListItem() {Text = "F", Value = "F", Selected = false})
            };
            ViewData["marks"] = marks;
            return View();
        }
        [HttpPost]
        public ActionResult Mark(Record marks, HttpPostedFileBase studentname, HttpPostedFileBase mark, HttpPostedFileBase feedback)
        {     //submit the mark information
            if (User.Identity.IsAuthenticated)              //user should login first
            {
               if(studentname!=null)
               {
                    marks.studentName = User.Identity.Name;
                    marks.marks = marks;
                    marks.feedback = feedback.ToString();

                    return RedirectToAction("AfterLogin", "Recordlist");
                }
                    ViewBag.Message = "studentname is not exist！";
       
                    RecordDB.Add(marks);
                    RContext.SaveChanges();
                    return View();
            }
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