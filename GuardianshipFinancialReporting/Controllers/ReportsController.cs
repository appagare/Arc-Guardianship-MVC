using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GuardianshipFinancialReporting.Models;
using Microsoft.AspNet.Identity;

namespace GuardianshipFinancialReporting.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private GuardianshipFinancialReportingDBContext db = new GuardianshipFinancialReportingDBContext();

        private IQueryable<Ward> userWards(string UserID)
        {
            return db.Wards.Where(w => w.UserID == UserID);
        }
        // GET: Reports
        //public ActionResult Index()
        //{
        //    string UserID = User.Identity.GetUserId();
        //    // var reports = db.Reports.Include(r => r.AspNetUser).Where(r => r.UserID == UserID);
        //    var reports = db.Reports.Where(r => r.UserID == UserID);
        //    reports = db.Reports.Where(r => r.UserID == UserID);
        //    return View(reports.ToList());
        //}
        public ActionResult Index(int? id)
        {
            string UserID = User.Identity.GetUserId();
            // var reports = db.Reports.Include(r => r.AspNetUser).Where(r => r.UserID == UserID);
            // var reports = db.Reports.Where(r => r.UserID == UserID);
            GuardianshipFinancialReporting.ViewModels.ReportViewModel rvm;
            if (id != null)
            {
                rvm = new ViewModels.ReportViewModel(UserID, id.Value);
            }
            else
            {
                rvm = new ViewModels.ReportViewModel(UserID);
            }
            return View(rvm);

        }

        //public ActionResult SelectWard()
        //{

        //    List<SelectListItem> items = new List<SelectListItem>();

        //    items.Add(new SelectListItem { Text = "Action", Value = "0" });


        //    items.Add(new SelectListItem { Text = "Drama", Value = "1" });

        //    items.Add(new SelectListItem { Text = "Comedy", Value = "2", Selected = true });

        //    items.Add(new SelectListItem { Text = "Science Fiction", Value = "3" });

        //    ViewBag.MovieType = items;

        //    //List<SelectListItem> listItems = new List<SelectListItem>();
        //    //listItems.Add(new SelectListItem
        //    //{
        //    //    Text = "",
        //    //    Value = "0"
        //    //});
        //    //foreach (var item in Model.WardsList.ToList())
        //    //{
        //    //    listItems.Add(new SelectListItem
        //    //    {
        //    //        Text = item.FullName,
        //    //        Value = item.WardID.ToString()
        //    //    });
        //    //}

        //    return View();

        //}

        // GET: Reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                // index when no id passed
                return RedirectToAction("Index");
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = report.UserID;
            return View(report);
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            // SQL 
            //select * from Reports r 
            // inner join Wards w 
            // on r.WardID=w.WardID 
            // where r.UserID=UserID


            // LINQ
            //IEnumerable<SelectListItem> stores =
            // from store in database.Stores
            // where store.CompanyID == curCompany.ID
            // select new SelectListItem { Value = store.Name, Text = store.ID };

            // Lambda
            //IEnumerable<SelectListItem> stores = 
            //   database.Stores
            //       .Where(store => store.CompanyID == curCompany.ID)
            //       .Select(store => new SelectListItem { Value = store.Name, Text = store.ID });

            string UserID = User.Identity.GetUserId();
            // var reports = db.Reports.Include(r => r.AspNetUser).Where(r => r.UserID == UserID);
            ViewBag.UserID = UserID;

            //   db.Reports
            //       .Where(w => w.CompanyID == curCompany.ID)
            //       .Select(store => new SelectListItem { Value = store.Name, Text = store.ID });

            // var wards = db.Wards.Where(w => w.UserID == UserID);
            ViewBag.WardID = new SelectList(userWards(UserID), "WardID", "FullName");

     return View();
 }

 // POST: Reports/Create
 // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
 // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
 [HttpPost]
 [ValidateAntiForgeryToken]
 public ActionResult Create([Bind(Include = "ReportID,WardID,UserID,PeriodStartMonth,PeriodStartYear,PeriodDuration,CreateDate,LastUpdated,SubmittedDate,DeletedDate")] Report report)
 {
     if (ModelState.IsValid)
     {
         report.CreateDate = DateTime.Now;
         report.LastUpdated = DateTime.Now;
         db.Reports.Add(report);
         db.SaveChanges();
         return RedirectToAction("Index");
     }

     ViewBag.UserID = User.Identity.GetUserId();
     ViewBag.WardID = new SelectList(userWards(report.UserID), "WardID", "FullName", report.WardID);
     return View(report);
 }

 // GET: Reports/Edit/5
 public ActionResult Edit(int? id)
 {
     if (id == null)
     {
         // index when no id passed
         return RedirectToAction("Index");
     }
     Report report = db.Reports.Find(id);
     if (report == null)
     {
         return HttpNotFound();
     }
     ViewBag.UserID = report.UserID;
     //var wards = db.Wards.Where(w => w.UserID == report.UserID);
     ViewBag.WardID = new SelectList(userWards(report.UserID), "WardID", "FullName");
     return View(report);
 }

 // POST: Reports/Edit/5
 // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
 // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
 [HttpPost]
 [ValidateAntiForgeryToken]
 public ActionResult Edit([Bind(Include = "ReportID,WardID,UserID,PeriodStartMonth,PeriodStartYear,PeriodDuration,CreateDate")] Report report)
 {
     if (ModelState.IsValid)
     {
        report.LastUpdated = DateTime.Now;
        db.Entry(report).State = EntityState.Modified;
         db.SaveChanges();
         return RedirectToAction("Index");
     }
     ViewBag.UserID = report.UserID;
     ViewBag.WardID = new SelectList(userWards(report.UserID), "WardID", "FullName");
     return View(report);
 }

 // GET: Reports/Delete/5
 public ActionResult Delete(int? id)
 {
     if (id == null)
     {
         // index when no id passed
         return RedirectToAction("Index");
     }
     Report report = db.Reports.Find(id);
     if (report == null)
     {
         return HttpNotFound();
     }
     ViewBag.UserID = report.UserID;
     return View(report);
 }

 // POST: Reports/Delete/5
 [HttpPost, ActionName("Delete")]
 [ValidateAntiForgeryToken]
 public ActionResult DeleteConfirmed(int id)
 {
     Report report = db.Reports.Find(id);
     db.Reports.Remove(report);
     db.SaveChanges();
     return RedirectToAction("Index");
 }

 protected override void Dispose(bool disposing)
 {
     if (disposing)
     {
         db.Dispose();
     }
     base.Dispose(disposing);
 }
}
    
}
