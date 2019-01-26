using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcwithEFMycatSample.Models;

namespace MvcwithEFMycatSample.Controllers
{
    public class HomeController : Controller
    {
        public MyDbContext _context { get; set; }
        public HomeController(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }

        public IActionResult Index()
        {
            var users = _context.Users.FirstOrDefault();

            //users.Name = "Yebin1";
            //_context.SaveChanges();

            _context.Database.ExecuteSqlCommand("INSERT INTO dt_users ( Name ) VALUES ('Yebin2') ");

            return View();
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
