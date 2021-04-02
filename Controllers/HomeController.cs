using Assignment_10.Models;
using Assignment_10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }


        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }


        public IActionResult Index(long? teamid, string teamname,  int pageNum = 0)
        {
            int pageSize = 5;

            return View(new IndexViewModel
            {
                Bowlers = (context.Bowlers
                .Where(x => x.TeamId == teamid || teamid == null)
                .OrderBy(x => x.BowlerFirstName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList()),

                pageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,

                    //If no team has been selected then get the full count of bowlers.
                    //If a team is selected, then only count the number of results for that filter.

                    TotalNumItems = (teamid == null ? context.Bowlers.Count() :
                        context.Bowlers.Where(x => x.TeamId == teamid).Count())

                },

                TeamName = teamname

            }) ;
               

            /*.FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId = {teamid} OR {teamid} IS NULL Order BY BowlerFirstName")*/
            /*.FromSqlRaw("SELECT * FROM Bowlers WHERE BowlerFirstName LIKE \'B%\' ORDER BY BowlerFirstName")*/
            /*.OrderBy(x => x.BowlerFirstName)*/
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
