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
        public ActionResult Upload()  //visit the upload view
        {
            if (User.Identity.IsAuthenticated) //user should login 
            {                                //before visit upload view
                if (!SorT()) {
                    return RedirectToAction("RecordList", "Lecturer");
                }
                return View();
            }
            else
                return RedirectToAction("Login","Account");
        }
        [HttpPost]
        public ActionResult Upload(Record r, HttpPostedFileBase file) { //submit the upload information
            if (User.Identity.IsAuthenticated)  //user should login first
            {
                if (SorT())                     //if user is a student
                {
                    if (file == null)
                    {
                        return View("../Index");
                    }                                              
                    string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString();
                    var path = Path.Combine(Request.MapPath("~/studentRecord/" + User.Identity.Name + "/" + dateTime + "/"),Path.GetFileName(file.FileName));
                    if (!file.ContentType.Equals("text/html"))          //Validation to make sure the file is a html file
                    {                                                   //in StudentController.cs
                        ModelState.AddModelError("type", "Error file, you should upload a html file!");  
                        return View();         //If the type of file is not html, add a model error message.
                    }
                    using (var transaction = RContext.Database.BeginTransaction()) //use transaction to guarantee database integrity.
                    {
                        try
                        {
                            if (Directory.Exists(Server.MapPath("~/studentRecord/" + User.Identity.Name + "/" + dateTime)) == false)
                            {       //if there is not the floder create a new floder
                                Directory.CreateDirectory(Server.MapPath("~/studentRecord/" + User.Identity.Name + "/" + dateTime));
                            }
                            file.SaveAs(path);    //save the file into a special directory.
                            var store = new UserStore<ApplicationUser>(RContext);
                            var manager = new UserManager<ApplicationUser>(store);
                            var user = manager.FindById(User.Identity.GetUserId());
                            user.recordSum = user.recordSum + 1;
                            manager.Update(user);              //Update the user in database
                            r.fileName = file.FileName;
                            r.studentName = User.Identity.Name; //
                            r.UserId = User.Identity.GetUserId();
                            r.marks = "Waiting";
                            var path1 = "studentRecord/" + User.Identity.Name + "/" + dateTime + "/" + Path.GetFileName(file.FileName);
                            r.filePath = path1;                               //Path1 will be stored into database and it will be used                                                   
                            RecordDB.Add(r);                                  //when we need to find the file
                            RContext.SaveChanges();
                            transaction.Commit();   // database transcation
                            return RedirectToAction("Result", "Student");
                        }
                        catch { transaction.Rollback();  }
                            
                        
                    }
                    return View();

                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index","Home");
        }
        public ActionResult Result() //visit the view of the results of student themselves
        {
            if (User.Identity.IsAuthenticated)
            {
                Record[] rr;
                if (SorT())  //if the user is a student
                    rr = RecordDB.FindByUserId(User.Identity.GetUserId());
                else
                    rr = RecordDB.ToArray();
                return View(rr);
            }
            else
                return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult Detail(int recordID,Record r) //return the view of Detail which student can see their html
        {     //submit the mark information
            Record record = RecordDB.FindByID(recordID);
            int id = record.ID;
            return View(record);
        }
        public Boolean SorT()     //the user is student or Lecturer
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




