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

namespace GuardianshipFinancialReporting.ViewModels
{
    public class ReportViewModel
    {
        private string userID { get; set; }
        public int SelectedWardID { get; }
        public IQueryable<GuardianshipFinancialReporting.Models.Report> ReportsList;
        public IQueryable<GuardianshipFinancialReporting.Models.Ward> WardsList;
        public ReportViewModel(string UserID)
        {
            userID = UserID;
            ReportsList = db.Reports.Where(r => r.UserID == UserID);
            // full wards list
            WardsList = db.Wards.Where(r => r.UserID == UserID);

        }
        public ReportViewModel(string UserID, int WardID)
        {
            userID = UserID;
            if (WardID > 0)
            {
                // filtered list
                SelectedWardID = WardID;
                ReportsList = db.Reports.Where(r => ((r.UserID == UserID) && (r.WardID == SelectedWardID)));
                
            }
            else
            {
                // all reports
                ReportsList = db.Reports.Where(r => r.UserID == UserID);
            }
            // full wards list
            WardsList = db.Wards.Where(r => r.UserID == UserID);

        }

        private GuardianshipFinancialReportingDBContext db = new GuardianshipFinancialReportingDBContext();
        public GuardianshipFinancialReporting.Models.Report  Report { get; set; }

        //public List<GuardianshipFinancialReporting.Models.Ward> Wards;


        //void foo()
        //{
        //    var x = Enum.GetName(typeof(ReportEnums.DurationTypes), 1);
        //    Dictionary<int, string> foo = new Dictionary<int, string>();
        //    foo.Add(1, "test");

        //    SortedSet<int> foo2 = new SortedSet<int>();
        //    foo2.Add(1);

        //    ICollection<int> foo3 = new HashSet<int>() ;

        //    HashSet<int> foo4 = new HashSet<int>();

        //    Tuple<int> t = new Tuple<int>(0);
        //    t.Item1.ToString();

        //    // System.Threading.Tasks.Parallel.ForEach<>();
                
              
        //}

    }
}