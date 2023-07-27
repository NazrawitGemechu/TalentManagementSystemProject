using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TalentManagement.Domain.Entities;
using TalentManagement.UI.Models;


namespace TalentManagement.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnurl=null)
        {
            if(!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                //create role
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));              
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.Company))
            {
                //create role
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Company));
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.Talent))
            {
                //create role
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Talent));
            }
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Value = "Admin",
                Text = "Admin"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "Company",
                Text = "Company"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "Talent",
                Text = "Talent"
            });
            ViewData["ReturnUrl"] = returnurl;
            RegisterViewModel registerViewModel = new RegisterViewModel()
            {
                RoleList = listItems
            };
            
            return View(registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FullName = model.FullName };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.RoleSelected != null && model.RoleSelected.Length > 0 && model.RoleSelected == "Admin")
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else if (model.RoleSelected != null && model.RoleSelected.Length > 0 && model.RoleSelected == "Company")
                    {
                        await _userManager.AddToRoleAsync(user, "Company");

                    }
                    else 

                    {
                        await _userManager.AddToRoleAsync(user, "Talent");

                    }
                   // await _signInManager.SignInAsync(user, isPersistent: false);
                    return View("RegisterCompleted");
                }

                AddErrors(result);
            }
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Value = "Admin",
                Text = "Admin"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "Company",
                Text = "Company"
               
            });
            listItems.Add(new SelectListItem()
            {
                Value = "Talent",
                Text = "Talent"
            });
            model.RoleList = listItems;
            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnurl=null)
        {
            ViewData["ReturnUrl"] = returnurl;


            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model,string returnurl=null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                   
                        return LocalRedirect(returnurl);
                    
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

            }

            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CompanyRegister(string returnurl = null)
        {
           
            if (!await _roleManager.RoleExistsAsync(UserRoles.Company))
            {
                //create role
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Company));
            }
          
           
            ViewData["ReturnUrl"] = returnurl;
            CompanyRegisterViewModel companyRegisterViewModel = new CompanyRegisterViewModel();

            return View(companyRegisterViewModel);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompanyRegister(CompanyRegisterViewModel model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName ,LastName=model.LastName,CompanyName=model.CompanyName};

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {                   
                        await _userManager.AddToRoleAsync(user, "Company");                                   
                    // await _signInManager.SignInAsync(user, isPersistent: false);
                    return View("RegisterCompleted");
                }

                AddErrors(result);
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> TalentRegister(string returnurl = null)
        {

            if (!await _roleManager.RoleExistsAsync(UserRoles.Company))
            {
                //create role
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Company));
            }


            ViewData["ReturnUrl"] = returnurl;
            TalentRegisterViewModel talentRegisterViewModel = new TalentRegisterViewModel();

            return View(talentRegisterViewModel);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TalentRegister(TalentRegisterViewModel model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FullName = model.FullName };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Talent");
                    // await _signInManager.SignInAsync(user, isPersistent: false);
                    return View("RegisterCompleted");
                }

                AddErrors(result);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(MainController.Home),"Main");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
            
           

        //    return View(model);
        //}

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);

            }
        }
       

    }
}
