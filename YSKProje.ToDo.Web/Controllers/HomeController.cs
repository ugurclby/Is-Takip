using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.Models;

namespace YSKProje.ToDo.Web.Controllers
{
    public class HomeController : BaseIdentityController
    {
        IGorevService _gorevService; 
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ICustomLogger _customLogger;
        public HomeController(IGorevService gorevService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, ICustomLogger customLogger) :base(userManager)
        {
            _gorevService = gorevService; 
            _signInManager = signInManager;
            _mapper = mapper;
            _customLogger = customLogger;
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

        public IActionResult ErrorPage (int? code)
        {
            if (code==404)
            {
                ViewBag.Code = 404;
                ViewBag.Message = "Sayfa Bulunamadı..!";
            }
            return View();
        }
        public IActionResult ProductError()
        {
            var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _customLogger.WriteLog($"Hata oluştu\n Hatanın Oluştuğu Dizin :{exceptionHandler.Path}\n Hata Mesajı :{exceptionHandler.Error.Message}\n Hata Stack Trace :{exceptionHandler.Error.StackTrace}");
              
            return View();
        }
        
        public void Hata()
        {
            throw new Exception("Hata Var ");
        }
    }
}
