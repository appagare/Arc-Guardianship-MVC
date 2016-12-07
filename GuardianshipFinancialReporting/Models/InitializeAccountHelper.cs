using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardianshipFinancialReporting.Models
{
    public static class InitializeAccountHelper
    {
         public static void InitializeNewUser(string UserID)
        {
            GuardianshipFinancialReportingDBContext db = new GuardianshipFinancialReportingDBContext();

            // get all records in DefaultCategory and copy them into the UserCategory table
            var defaultCategories = db.DefaultCategories.ToList().OrderBy(d => d.CategoryType).ThenBy(d => d.Ordinal);
            foreach (var result in defaultCategories)
            {
                UserCategory u = new Models.UserCategory();
                u.CategoryName = result.CategoryName;
                u.CategoryClass = result.CategoryClass;
                u.CategoryType = result.CategoryType;
                u.Ordinal = result.Ordinal; 
                u.Hide = false;
                u.UserID = UserID;
                db.UserCategories.Add(u);
            }
            // get all records in the DefaultSettings table and copy them into the UserSettings table
            var defaultSettings = db.DefaultSettings.ToList();
            foreach (var result in defaultSettings)
            {
                UserSetting u = new Models.UserSetting();
                u.Group  = result.SystemGroup;
                u.Setting  = result.SystemParameter;
                switch (result.SystemValue)
                {
                    case "[CURRENT_YEAR]":
                        u.Value = DateTime.Now.Year.ToString();
                        break;
                    default:
                        u.Value = result.SystemValue;
                        break;
                }
                u.UserID = UserID;
                db.UserSettings.Add(u);
            }
            
            db.SaveChanges();
            
        }
    }
}