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
        [AllowAnonymous]
        public IActionResult Home()
        {
            return View();
        }
        [Authorize(Roles="Admin")]
        public IActionResult List()
        {
            return View();
        }
    
        public  IActionResult How()
        {

            return View();
        }
        public IActionResult AccessDenied()
        {

            return View();
        }

    }
}
