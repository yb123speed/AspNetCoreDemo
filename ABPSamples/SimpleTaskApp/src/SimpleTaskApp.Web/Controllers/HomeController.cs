using Microsoft.AspNetCore.Mvc;
using SimpleTaskApp.People;

namespace SimpleTaskApp.Web.Controllers
{
    public class HomeController : SimpleTaskAppControllerBase
    {
        private readonly IPersonAppService _peopleAppService;
        public HomeController(IPersonAppService personAppService)
        {
            _peopleAppService = personAppService;
        }

        public ActionResult Index()
        {
            _peopleAppService.DoSomeStuff();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}