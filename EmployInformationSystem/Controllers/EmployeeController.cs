using EmployInformationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployInformationSystem.Controllers
{
    public class EmployeeController : Controller
    {
        #region Property
        List<Education> educationLst = new List<Education>();
        List<Employment> EmploymentLst = new List<Employment>();
        List<string> code = new List<string>();
        #endregion
        #region Action Method
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add(int? id)
        {
            ApplicationInfo appInfoObj = new ApplicationInfo();
            using (EmployeeDbContext dc = new EmployeeDbContext())
            {
                var appInfo = dc.ApplicationInfoDB.Include("Contact").Where(a => a.Id == id).FirstOrDefault();
                if (appInfo != null)
                {
                    appInfoObj = appInfo;
                }
                appInfoObj.DesiredChkItems = JobType();
                return View(appInfoObj);
            }
        }

        [HttpPost]
        public ActionResult Add(ApplicationInfo applicationInfo)
        {
            using (EmployeeDbContext db = new EmployeeDbContext())
            {

                db.Entry(applicationInfo.Contact).State = applicationInfo.Contact.Id == 0 ? EntityState.Added : EntityState.Modified;
                db.SaveChanges();

                applicationInfo.ContactId = applicationInfo.Contact.Id;
                applicationInfo.ApplyDate = DateTime.Now;
                applicationInfo.SignDate = DateTime.Now;
                db.Entry(applicationInfo).State = applicationInfo.Id == 0 ? EntityState.Added : EntityState.Modified;
                db.SaveChanges();

                var educationLst = (List<Education>)Session["educationLst"];
                if (educationLst != null)
                {
                    educationLst.ForEach(x => { x.Id = 0; x.ApplicationId = applicationInfo.Id; });
                    applicationInfo.Educations = educationLst.ToList();
                    db.EducationDB.AddRange(applicationInfo.Educations.ToList());
                    db.ChangeTracker.DetectChanges();
                    db.SaveChanges();
                    db?.Dispose();
                    Session["educationLst"] = null;
                }

                var employmentLst = (List<Employment>)Session["employmentLst"];
                if (employmentLst != null)
                {
                    employmentLst.ForEach(x => { x.Id = 0; x.ApplicationId = applicationInfo.Id; });
                    applicationInfo.Employments = employmentLst.ToList();
                    db.EmploymentDB.AddRange(applicationInfo.Employments.ToList());
                    db.ChangeTracker.DetectChanges();
                    db.SaveChanges();
                    db?.Dispose();
                    Session["employmentLst"] = null;
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult AddEducation(int? Id)
        {
            if (Session["employmentLst"] != null)
            {
                var education = (List<Education>)Session["educationLst"];
                var v = education.Where(x => x.Id == Id).FirstOrDefault();
                return View(v);
            }
            else
            {
                using (EmployeeDbContext dc = new EmployeeDbContext())
                {
                    var v = dc.EducationDB.Where(a => a.Id == Id).FirstOrDefault();
                    v.IsChanged = true;
                    return View(v);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEducation(Education education)
        {
            bool status = false;
            if (Session["educationLst"] == null)
            {
                Session["educationLst"] = new List<Education>();
            }
            var educations = (List<Education>)Session["educationLst"];
            var edu = educations.Where(x => x.Id == education.Id).FirstOrDefault();
            if (edu != null)
            {
                var index = educations.FindIndex(x => x.Id == education.Id);
                educations[index] = new Education()
                {
                    Id = education.Id,
                    Name = education.Name,
                    Degree = education.Degree,
                    Percentage = education.Percentage,
                    Year = education.Year
                };
                status = true;
            }
            else
            {
                if (education.IsChanged == false)
                    education.Id = educations.Count() + 1;
                educations.Add(education);
                status = true;
            }
            return new JsonResult
            {
                Data = new
                {
                    status = status
                }
            };
        }

        [HttpGet]
        public ActionResult DeleteEducation(int? Id)
        {
            if (Session["educationLst"] == null)
            {
                Session["educationLst"] = new List<Education>();
            }
            var educations = (List<Education>)Session["educationLst"];
            var edu = educations.Where(x => x.Id == Id).FirstOrDefault();
            if (edu != null)
            {
                return View(edu);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult ConfrmDeleteEducation(Education education)
        {
            bool status = false;
            if (Session["educationLst"] == null)
            {
                Session["educationLst"] = new List<Education>();
            }
            var educations = (List<Education>)Session["educationLst"];
            var edu = educations.Where(x => x.Id == education.Id).FirstOrDefault();
            if (edu != null)
            {
                educations.Remove(edu);
                status = true;
            }
            else
            {
                //education.Id = educations.Count() + 1;
                //educations.Add(education);
                //status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult AddEmployment(int? Id)
        {
            if (Session["employmentLst"] != null)
            {
                var employee = (List<Employment>)Session["employmentLst"];
                var v = employee.Where(x => x.Id == Id).FirstOrDefault();
                return View(v);
            }
            else
            {
                using (EmployeeDbContext dc = new EmployeeDbContext())
                {
                    var v = dc.EmploymentDB.Where(a => a.Id == Id).FirstOrDefault();
                    v.IsChanged = true;
                    return View(v);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployment(Employment employment)
        {
            bool status = false;

            if (Session["employmentLst"] == null)
            {
                Session["employmentLst"] = new List<Employment>();
            }
            var events = (List<Employment>)Session["employmentLst"];
            var emp = events.Where(x => x.Id == employment.Id).FirstOrDefault();
            if (emp != null)
            {
                var index = events.FindIndex(x => x.Id == employment.Id);
                events[index] = new Employment()
                {
                    Id = employment.Id,
                    Designation = employment.Designation,
                    EndDate = employment.EndDate,
                    Name = employment.Name,
                    StartDate = employment.StartDate
                };
                status = true;
            }
            else
            {
                if (employment.IsChanged == false)
                    employment.Id = events.Count() + 1;
                events.Add(employment);
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult DeleteEmployment(int? Id)
        {
            if (Session["employmentLst"] == null)
            {
                Session["employmentLst"] = new List<Employment>();
            }
            var employment = (List<Employment>)Session["employmentLst"];
            var emp = employment.Where(x => x.Id == Id).FirstOrDefault();
            if (emp != null)
            {
                return View(emp);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult ConfrmDeleteEmployment(Employment employment)
        {
            bool status = false;
            if (Session["employmentLst"] == null)
            {
                Session["employmentLst"] = new List<Employment>();
            }
            var employments = (List<Employment>)Session["employmentLst"];
            var emp = employments.Where(x => x.Id == employment.Id).FirstOrDefault();
            if (emp != null)
            {
                employments.Remove(emp);
                status = true;
            }
            else
            {
                //education.Id = educations.Count() + 1;
                //educations.Add(education);
                //status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
        #endregion
        #region Ajax Method
        public JsonResult GetEmployees()
        {
            using (EmployeeDbContext dc = new EmployeeDbContext())
            {
                var applications = dc.ApplicationInfoDB.Include("Contact").OrderBy(a => a.Contact.FirstName).ToList();
                return Json(new { data = applications }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetEducation(int? Id)
        {
            if (Session["educationLst"] == null)
            {
                using (EmployeeDbContext dc = new EmployeeDbContext())
                {
                    var educations = dc.EducationDB.Where(x => x.ApplicationId == Id).ToList();
                    return Json(new { data = educations }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { data = Session["educationLst"] }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetEmployment(int? Id)
        {
            if (Session["employmentLst"] == null)
            {
                using (EmployeeDbContext dc = new EmployeeDbContext())
                {
                    var employments = dc.EmploymentDB.Where(x => x.ApplicationId == Id).ToList();
                    return Json(new { data = employments }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { data = Session["employmentLst"] }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Private Method
        public List<CheckBoxModel> JobType()
        {
            List<CheckBoxModel> ChkItem = new List<CheckBoxModel>()
                {
                  new CheckBoxModel {Value=1,Text="Full Time",IsChecked=true },
                  new CheckBoxModel {Value=2,Text="Part Time",IsChecked=false },
                  new CheckBoxModel {Value=3,Text="Both",IsChecked=false }
                };
            return ChkItem;
        }
        #endregion
    }
}