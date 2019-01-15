using Microsoft.AspNetCore.Mvc;

namespace SimpleTaskApp.Web.Controllers
{
    public class HomeController : SimpleTaskAppControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}