using Microsoft.AspNetCore.Mvc;

namespace Tennisclubb.Controllers
{
    public class UpcomingSchedules : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
