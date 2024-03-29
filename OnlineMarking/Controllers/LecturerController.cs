﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineMarking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineMarking.Controllers
{
    public class LecturerController : Controller
    {
        private ApplicationDbContext RContext;
        private DbSet<Record> RecordDB;
        private Record[] Records;
        List<SelectListItem> marks = new List<SelectListItem>()
        { //DropDownList to select a grade
            (new SelectListItem() {Text = "A", Value = "A", Selected = false}),
            (new SelectListItem() {Text = "B", Value = "B", Selected = false}),
            (new SelectListItem() {Text = "C", Value = "C", Selected = false}),
            (new SelectListItem() {Text = "D", Value = "D", Selected = false}),
            (new SelectListItem() {Text = "F", Value = "F", Selected = false})
        };// in LecturerController.cs
        public LecturerController()
        {
            RContext = new Models.ApplicationDbContext();
            RecordDB = RContext.RecordDB;
            Records = RecordDB.ToArray();
        }
        // GET: Lecturer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RecordList()
        {     //view all the record that uploaded by students             
            if (User.Identity.IsAuthenticated)
            {
                Record[] rr;
                if (SorT()) //if the user is a student
                    return RedirectToAction("Result", "Student");
                rr = RecordDB.ToArray(); 
                return View(rr);
            }
            else
                return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult RecordList(int recordid)                                    
        {
            return RedirectToAction("Mark", "Lecturer", new { @id = recordid });
        }
        [HttpGet]
        public ActionResult Mark(int id)                                                //the record detail and Lecturers are able to mark in this view.
        {
            var marks = new List<SelectListItem>()                                      //A List of grades, which will generate a dropdownlist.
            {
                (new SelectListItem() {Text = "A", Value = "A", Selected = false}),
                (new SelectListItem() {Text = "B", Value = "B", Selected = false}),
                (new SelectListItem() {Text = "C", Value = "C", Selected = false}),
                (new SelectListItem() {Text = "D", Value = "D", Selected = false}),
                (new SelectListItem() {Text = "F", Value = "F", Selected = false})
            };
            Record r = RecordDB.FindByID(id);
            ViewData["marks"] = marks;
            return View(r);
        }
        [HttpPost]
        public ActionResult Mark(int ID, Record r) //process record which is marked by lecturer.
        {     

            if (!User.Identity.IsAuthenticated) //user should login first.
            {
                return RedirectToAction("Login", "Account");
            }
            
            Record record = RecordDB.FindByID(r.ID);
            record.marks = r.marks;
            record.feedback = r.feedback;
            ViewData["marks"] = marks;
            if (!ModelState.IsValid) {              //if both of the marks textbox and feedback textare are empty.
                if (record.feedback == null)
                    ModelState.AddModelError("feedback", "feedback cannot be null.");
                return View(record);
            }
            if (record.feedback == null)
            {                                   //if the marksfeedback textare are empty.
                ModelState.AddModelError("feedback", "feedback cannot be null.");
                return View(record);
            }
            RContext.Entry<Record>(record).State = EntityState.Modified;    //update the record
            RContext.SaveChanges();
            return RedirectToAction("RecordList", "Lecturer");
        }

        public Boolean SorT()                                                           //make sure the user is student or Lecturer
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