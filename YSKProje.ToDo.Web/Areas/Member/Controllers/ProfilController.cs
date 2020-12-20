using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class ProfilController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public ProfilController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "Profil";
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUserListViewModel model = new AppUserListViewModel();
            model.Id = appUser.Id;
            model.Name = appUser.Name;
            model.SurName = appUser.Surname;
            model.Picture = appUser.Picture;
            model.EMail = appUser.Email;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserListViewModel model, IFormFile resim)
        {
            TempData["Active"] = "Profil";
            if (ModelState.IsValid)
            {
                var guncellencekKullanici = _userManager.Users.FirstOrDefault(I => I.Id == model.Id);
                if (resim != null)
                {
                    string uzanti = Path.GetExtension(resim.FileName);
                    string resimAd = Guid.NewGuid() + uzanti;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + resimAd);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await resim.CopyToAsync(stream);
                    }

                    guncellencekKullanici.Picture = resimAd;
                }

                guncellencekKullanici.Name = model.Name;
                guncellencekKullanici.Surname = model.SurName;
                guncellencekKullanici.Email = model.EMail;

                var result = await _userManager.UpdateAsync(guncellencekKullanici);
                if (result.Succeeded)
                {
                    TempData["message"] = "Güncelleme işleminiz başarı ile gerçekleşti";
                    return RedirectToAction("Index");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }
    }
}