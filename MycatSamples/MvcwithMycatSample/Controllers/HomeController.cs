using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcwithEFMycatSample.Models;

namespace MvcwithEFMycatSample.Controllers
{
    public class HomeController : Controller
    {
        public MyDbContext _context { get; set; }

        private static MySql.Data.MySqlClient.MySqlConnection GetMySqlConnection(bool open = true,
            bool convertZeroDatetime = false, bool allowZeroDatetime = false)
        {

            string cs = "server=192.168.1.180;port=8066;uid=root;pwd=123456;database=testdb";
            var csb = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder(cs);
            csb.AllowZeroDateTime = allowZeroDatetime;
            csb.ConvertZeroDateTime = convertZeroDatetime;
            var conn = new MySql.Data.MySqlClient.MySqlConnection(csb.ConnectionString);
            if (open) conn.Open();
            return conn;
        }
        public HomeController(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }

        public IActionResult Index()
        {
            //var users = _context.Users.FirstOrDefault();

            //users.Name = "Yebin1";
            //_context.SaveChanges();

            //_context.Database.ExecuteSqlCommand("INSERT INTO dt_users ( Name ) VALUES ('Yebin2') ");
            using (var conn = GetMySqlConnection())
            {
                var user = conn.QueryFirstOrDefault<User>("SELECT * FROM dt_users;");
                Console.WriteLine("Query Completed!");
                Console.WriteLine(user.Name + ":" + user.ID);
                int r = conn.Execute("INSERT INTO dt_users ( Name ) VALUES ('Yebin2')");
                Console.WriteLine("Insert Completed!");
                int r1 = conn.Execute("UPDATE dt_users SET Name=@a WHERE ID=@b", new { a = "Changed", b = user.ID });
                Console.WriteLine("UPDATE Completed!");
                int r2 = conn.Execute("Delete From dt_users WHERE ID=@a", new { a = user.ID });
                Console.WriteLine("Delete Completed!");

            }

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
