using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Models;

namespace YSKProje.ToDo.Web.Controllers
{
    public class HomeController : Controller
    {
        IGorevService _gorevService;
        UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        public HomeController(IGorevService gorevService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _gorevService = gorevService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View(); 
        }

        public async Task<IActionResult> GirisYap(AppUserSignInDto appUserSignInModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(appUserSignInModel.UserName);
                if (user !=null)
                {
                    var identityResult= await _signInManager.PasswordSignInAsync(appUserSignInModel.UserName, appUserSignInModel.Password, appUserSignInModel.RememberMe, false);

                    if (identityResult.Succeeded)
                    {
                        var roles= await _userManager.GetRolesAsync(user);
                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home", new { area = "Member" });
                        }
                    }
                }
                ModelState.AddModelError("", "Kullanıcı Adı Veya Şifreniz Hatalı..!");
            }
            return View("Index",appUserSignInModel);
        }
        public IActionResult KayitOl()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> KayitOl(AppUserViewDto appUserViewModel)
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

        public async Task<IActionResult> CikisYap()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

    }
}
