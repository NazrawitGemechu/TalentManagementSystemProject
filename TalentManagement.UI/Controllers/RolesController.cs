using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TalentManagement.Persistance.Data;
using TalentManagement.Domain.Entities;
using TalentManagement.UI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace TalentManagement.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {

         private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = _db.Roles.ToList();

            return View(roles);
        }
        [HttpGet]
        public IActionResult Upsert(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return View();
            }
            else
            {
                //update
                var objFromDb = _db.Roles.FirstOrDefault(u => u.Id == id);
                return View(objFromDb);
            }



        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(IdentityRole roleObj)
        {
            if (roleObj.Name!=null)
            {
                if (await _roleManager.RoleExistsAsync(roleObj.Name))
                {
                    //error
                    TempData[SD.Error] = "Role already exists";
                }
                if (string.IsNullOrEmpty(roleObj.Id))
                {
                    //create
                    await _roleManager.CreateAsync(new IdentityRole() { Name = roleObj.Name });
                    TempData[SD.Success] = "Role created successfully";
                }
                else
                {
                    //update
                    var objRoleFromDb = _db.Roles.FirstOrDefault(u => u.Id == roleObj.Id);
                    if (objRoleFromDb == null)
                    {
                        TempData[SD.Error] = "Role not found";
                        return RedirectToAction(nameof(Index));
                    }
                    objRoleFromDb.Name = roleObj.Name;
                    objRoleFromDb.NormalizedName = roleObj.Name.ToUpper();
                    var resutl = await _roleManager.UpdateAsync(objRoleFromDb);
                    TempData[SD.Success] = "Role updated successfully";

                }
                return RedirectToAction(nameof(Index));
            }
            return View(roleObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var objFromDb = _db.Roles.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var userRolesForThisRole = _db.UserRoles.Where(u => u.RoleId == id).Count();
            if (userRolesForThisRole > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            await _roleManager.DeleteAsync(objFromDb);
            return RedirectToAction(nameof(Index));

        }

    }
}