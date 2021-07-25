using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sample_mvc5.Models;
using System.Data.Entity;


namespace sample_mvc5.Controllers
{
    public class HomeController : Controller
    {
        private Loginpages studtlist = new Loginpages();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Studentclass detail)
        {
            var credential = studtlist.Studentmarks.Where(x => x.Username == detail.Username && x.Password == detail.Password).Count();
            if (credential > 0)
            {
                return RedirectToAction("Dashboard", detail);
            }
            else
            {
                ViewBag.nodata = "please Enter your details to add";
               // ModelState.AddModelError("","Invalid username and password");
                return RedirectToAction("Create");
            }

            
        }
        
        public ActionResult Create()
        {   
          return View();
        }
        #region Create and Update
        [HttpPost]
        public ActionResult Create(Studentclass detail)
        {
            //we can store any class in var using tempData and ViewBag but not methods within the class
            
            //storing methods within class ==
            //var ourmodel=TempData["datda"] as Loginpages.Studentclass;

            //storing class==
            var ourmodel = ViewBag.name as Studentclass;
            var ourmodel1 = TempData["model"] as Studentclass;
            var ourmodel2 = ViewBag.name as Loginpages;


            var updatdetail = ourmodel2.Studentmarks.Where(model => model.Student_Id == detail.StudentId).FirstOrDefault();
            if (updatdetail != null)
            {
                updatdetail.Student_Id = detail.StudentId;
                updatdetail.First_Name = detail.firstname;
                updatdetail.Last_Name = detail.secondname;
                updatdetail.Student_Dob = detail.Studentsdob;
                updatdetail.Age = detail.Age;
                updatdetail.Favorite_Subject = detail.Favoritesubject;
                updatdetail.Interested_Course = detail.InterestedCourse;
                updatdetail.Maths_Mark = detail.Mathsmark;
                updatdetail.Chemistry_Mark = detail.ChemistryMark;
                updatdetail.Comp_Mark = detail.CompMark;
                updatdetail.Updated_Time_Stamp = DateTime.Now;
                updatdetail.Username = detail.Username;
                updatdetail.Password = detail.Password;
                studtlist.SaveChanges();
            }
            else
            { 
                Studentmark addlist = new Studentmark();
                addlist.First_Name = detail.firstname;
                addlist.Last_Name = detail.secondname;
                addlist.Student_Dob = detail.Studentsdob;
                addlist.Age = detail.Age;
                addlist.Favorite_Subject = detail.Favoritesubject;
                addlist.Interested_Course = detail.InterestedCourse;
                addlist.Maths_Mark = detail.Mathsmark;
                addlist.Chemistry_Mark = detail.ChemistryMark;
                addlist.Comp_Mark = detail.CompMark;
                addlist.Is_Deleted = false;
                addlist.Created_Time_Stamp = DateTime.Now;
                addlist.Updated_Time_Stamp = DateTime.Now;
                addlist.Username = detail.Username;
                addlist.Password = detail.Password;
                studtlist.Studentmarks.Add(addlist);
                studtlist.SaveChanges();
            }
            return RedirectToAction("Dashboard", detail);
        }
        #endregion
        #region Dashboard
        public ActionResult Dashboard(Studentclass detail)
        {
            if (detail != null)
            {
                List<Studentclass> lists = new List<Studentclass>();
                var checking = studtlist.Studentmarks.Where(y => y.Username == detail.Username && y.Password == detail.Password || y.Is_Deleted == false).ToList();
                foreach (var datalist in checking)
                {
                    Studentclass listobj = new Studentclass();
                    listobj.StudentId = datalist.Student_Id;
                    listobj.firstname = datalist.First_Name;
                    listobj.secondname = datalist.Last_Name;
                    listobj.Studentsdob = datalist.Student_Dob;
                    listobj.Age = datalist.Age;
                    listobj.Favoritesubject = datalist.Favorite_Subject;
                    listobj.InterestedCourse = datalist.Interested_Course;
                    listobj.Mathsmark = datalist.Maths_Mark;
                    listobj.ChemistryMark = datalist.Chemistry_Mark;
                    listobj.CompMark = datalist.Comp_Mark;
                    listobj.Username = datalist.Username;
                    listobj.Password = datalist.Password;
                    lists.Add(listobj);
                }
                return View(lists);

            }
            return View();
        }
        #endregion
        #region Delete
        public ActionResult Delete(int studentid)
        {
            using (var stud = new Loginpages())
            {
                var checking = studtlist.Studentmarks.Where(model => model.Student_Id == studentid).FirstOrDefault();
                checking.Is_Deleted = true;
                studtlist.SaveChanges();
                return RedirectToAction("Dashboard");
            }
        }
        #endregion
        #region Edit
        public ActionResult Edit(int studentid)
        {
            Studentclass detail = new Studentclass();
            var searching = studtlist.Studentmarks.Where(model => model.Student_Id == studentid).FirstOrDefault();
            if (searching != null)
            {
                detail.StudentId = searching.Student_Id;
                detail.firstname = searching.First_Name;
                detail.secondname = searching.Last_Name;
                detail.Studentsdob = searching.Student_Dob;
                detail.Age = searching.Age;
                detail.Favoritesubject = searching.Favorite_Subject;
                detail.InterestedCourse = searching.Interested_Course;
                detail.Mathsmark = searching.Maths_Mark;
                detail.ChemistryMark = searching.Chemistry_Mark;
                detail.CompMark = searching.Comp_Mark;
                detail.Username = searching.Username;
                detail.Password = searching.Password;
            }
            return View("Create", detail);

        }

        #endregion

    }
}