using System;
using Microsoft.AspNetCore.Mvc;
using TechJobsMVCAutograded6.Controllers;
using TechJobsMVCAutograded6.Models;

namespace AutogradingTests;

[TestClass]
public class TestTaskFour
{
    public TestTaskFour()
    {
    }

    // Test that Results method exists and returns a view.
    [TestMethod]
    public void TestResultsExistsAndLoadsView()
    {
       SearchController _controller = new SearchController();
       var result = _controller.Results("coreCompetency", "Ruby");
       var typeCheck = result as ViewResult;
       Assert.IsNotNull(typeCheck);
    }

    // Test that Results method returns the appropriate number of results for different searches
    [DataTestMethod]
    [DataRow("location", "new york", 1)]
    [DataRow("location", "chicago", 0)]
    [DataRow("employer", "equifax", 1)]
    [DataRow("all", "ruby", 4)]
    [DataRow("coreCompetency", "Ruby", 3)]
    public void TestResultsReturnsCorrectNumber(string searchType, string searchTerm, int expected)
    {
       SearchController _controller = new SearchController();
       var result = _controller.Results(searchType, searchTerm) as ViewResult;
       var jobs = (List<Job>)result.ViewData["jobs"];
       Assert.AreEqual(expected, jobs.Count);
    }

}

