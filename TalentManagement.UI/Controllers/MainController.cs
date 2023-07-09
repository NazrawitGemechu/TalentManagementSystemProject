using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TalentManagement.UI.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult List()
        {
            return View();
        }
        [Authorize]
        public  IActionResult How()
        {

            return View();
        }
    }
}
