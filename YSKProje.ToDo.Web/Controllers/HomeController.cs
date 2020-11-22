using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Models;

namespace YSKProje.ToDo.Web.Controllers
{
    public class HomeController : Controller
    {
        IGorevService _gorevService;
        UserManager<AppUser> _userManager;
        public HomeController(IGorevService gorevService, UserManager<AppUser> userManager)
        {
            _gorevService = gorevService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(); 
        }
        public IActionResult KayitOl()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> KayitOl(AppUserViewModel appUserViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    Name= appUserViewModel.Name,
                    Surname = appUserViewModel.SurName,
                    UserName = appUserViewModel.UserName,
                    Email = appUserViewModel.Email 
                };

               var result = await _userManager.CreateAsync(appUser, appUserViewModel.Password);

                if (result.Succeeded)
                {
                    var roleResult= await _userManager.AddToRoleAsync(appUser, "Member");
                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("GirisYap");
                    }
                    foreach (var item in roleResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(appUserViewModel);
        }

    }
}
