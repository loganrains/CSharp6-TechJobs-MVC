using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Mvc;
using TechJobsMVCAutograded6.Data;
using TechJobsMVCAutograded6.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsMVCAutograded6.Controllers;

public class ListController : Controller
{
    internal static Dictionary<string, string> ColumnChoices = new Dictionary<string, string>()
        {
            {"all", "All"},
            {"employer", "Employer"},
            {"location", "Location"},
            {"positionType", "Position Type"},
            {"coreCompetency", "Skill"}
        };
    internal static Dictionary<string, List<JobField>> TableChoices = new Dictionary<string, List<JobField>>()
        {
            //{"all", "View All"},
            {"employer", JobData.GetAllEmployers()},
            {"location", JobData.GetAllLocations()},
            {"positionType", JobData.GetAllPositionTypes()},
            {"coreCompetency", JobData.GetAllCoreCompetencies()}
        };

    public IActionResult Index()
    {
        ViewBag.columns = ColumnChoices;
        ViewBag.tableChoices = TableChoices;
        ViewBag.employers = JobData.GetAllEmployers();
        ViewBag.locations = JobData.GetAllLocations();
        ViewBag.positionTypes = JobData.GetAllPositionTypes();
        ViewBag.skills = JobData.GetAllCoreCompetencies();

        return View();
    }

    // TODONE #2 - Complete the Jobs action method
    public IActionResult Jobs(string column, string value)
    {
        List<Job> jobs = new();

        if (value == "View All")
        {
            jobs = JobData.FindAll();
            ViewBag.title = "ALL JOBS";
        }
        else
        {
            jobs = JobData.FindByColumnAndValue(column, value);
            
            if (column == "positionType")
            {
                ViewBag.title = $"JOBS FOR POSITION TYPE: {value.ToUpper()}";
            }
            else
            {
                ViewBag.title = $"JOBS FOR {column.ToUpper()}: {value.ToUpper()}";
            }
        }
        
        ViewBag.Jobs = jobs;

        return View();
    }
}