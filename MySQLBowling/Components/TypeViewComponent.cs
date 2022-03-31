using Microsoft.AspNetCore.Mvc;
using MySQLBowling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySQLBowling.Components
{
    public class TypeViewComponent : ViewComponent
    {
        private BowlingDbContext _context { get; set; }

        //Constructor
        public TypeViewComponent(BowlingDbContext temp)
        {
            _context = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["team"];

            var type = _context.Bowlers
                .Select(x => x.Team.TeamName)
                .Distinct();

            return View(type);
        }
    }
}
