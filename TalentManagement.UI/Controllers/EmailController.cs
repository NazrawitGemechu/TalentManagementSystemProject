//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using TalentManagement.UI.Migrations;
//using TalentManagement.UI.Models.Identity;

//namespace TalentManagement.UI.Controllers
//{
//    public class EmailController : Controller
//    {
//       // private UserManager<ApplicationUser> userManager;
//        private readonly UserManager<IdentityUser> _userManager;
//        public EmailController(UserManager<IdentityUser> userManager)
//        {
//            _userManager = userManager;
//        }

//        public async Task<IActionResult> ConfirmEmail(string token, string email)
//        {
//            var user = await _userManager.FindByEmailAsync(email);
//            if (user == null)
//                return View("Error");

//            var result = await _userManager.ConfirmEmailAsync(user, token);
//            return View(result.Succeeded ? "ConfirmEmail" : "Error");
//        }
//    }
//}
