using Assignment_10.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_10.Components
{
    public class BowlerTeamViewComponent : ViewComponent
    {
        private BowlingLeagueContext context;
        public BowlerTeamViewComponent(BowlingLeagueContext ctx)
        {
            context = ctx;
        }

        //used to set up the data for the view component with the Team information. Used to make filtering.
        public IViewComponentResult Invoke()
        {

            //this is used to make the highlighing of the filters possible.

            ViewBag.SelectedTeamName = RouteData?.Values["teamname"];

            return View(context.Teams
                .Distinct()
                .OrderBy(x=>x));

           /* .Select(x => x.TeamName)
                .Distinct()
                .OrderBy(x => x)
                .ToList()*/
        }
    }
}
