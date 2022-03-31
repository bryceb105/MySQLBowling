using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySQLBowling.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MySQLBowling.Controllers
{
    public class HomeController : Controller
    {
        private BowlingDbContext _context { get; set; }

        //Constructor
        public HomeController(BowlingDbContext temp)
        {
            _context = temp;
        }

        public IActionResult Index(string team)
        {
            var blah = _context.Bowlers
                .Include(x => x.Team)
                .Where(x => x.Team.TeamName == team || team == null)
                .ToList();


            return View(blah);
        }

        //Input Bowler
        [HttpGet]
        public IActionResult BowlerInput ()
        {
            ViewBag.Teams = _context.Teams.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult BowlerInput (Bowler b)
        {
            _context.Add(b);
            _context.SaveChanges();

            return View("Confirmation", b);
        }

        //Delete
        [HttpGet]
        public IActionResult Delete(int bowlerid)
        {
            var application = _context.Bowlers.Single(x => x.BowlerID == bowlerid);

            return View(application);
        }

        //remove entry
        [HttpPost]
        public IActionResult Delete(Bowler bowler)
        {
            _context.Bowlers.Remove(bowler);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int bowlerid)
        {
            int j = 1;

            ViewBag.Teams = _context.Teams.ToList();

            var bowler = _context.Bowlers.Single(x => x.BowlerID == bowlerid);

            return View("BowlerInput", bowler);
        }
        // edit entry
        [HttpPost]
        public IActionResult Edit(Bowler info)
        {
            _context.Update(info);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
