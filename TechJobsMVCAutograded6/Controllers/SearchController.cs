using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechJobsMVCAutograded6.Data;
using TechJobsMVCAutograded6.Models;

namespace TechJobsMVCAutograded6.Controllers;

public class SearchController : Controller
{
    // GET: /<controller>/
    public IActionResult Index()
    {
        ViewBag.columns = ListController.ColumnChoices;
        return View();
    }

    // TODONE #3 - Create an action method to process a search request and render the updated search views.
    // GET: /<controller>/
    public IActionResult Results(string searchType, string searchTerm)
    {    
        List<Job> jobs = new();

        if (searchTerm == "all" || searchTerm == "" || searchTerm == null)
        {
            jobs = JobData.FindAll();
            ViewBag.title = "ALL JOBS";
        }
        else if (searchType.ToLower() == "all")
        {
            jobs = JobData.FindByValue(searchTerm);
            ViewBag.title = $"ALL JOBS {searchTerm.ToUpper()}";
        }
        else
        {
            jobs = JobData.FindByColumnAndValue(searchType, searchTerm);
            
            if (searchType == "positionType")
            {
                ViewBag.title = $"JOBS FOR POSITION TYPE: {searchType.ToUpper()}";
            }
            else
            {
                ViewBag.title = $"JOBS FOR {searchType.ToUpper()}: {searchTerm.ToUpper()}";
            }
        }

        ViewBag.Jobs = jobs;
        ViewBag.columns = ListController.ColumnChoices;

        return View("Index");
    }
}

