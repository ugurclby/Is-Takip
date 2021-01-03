using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Area(AreaInfo.Member)]
    [Authorize(Roles = RoleInfo.Member)]
    public class ProfilController : BaseIdentityController
    { 
        private readonly IMapper _mapper;
        public ProfilController(UserManager<AppUser> userManager, IMapper mapper):base(userManager)
        { 
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = MenuInfo.Profil;
            var appUser = await GetirUserManager();
            //AppUserListViewModel model = new AppUserListViewModel();
            //model.Id = appUser.Id;
            //model.Name = appUser.Name;
            //model.SurName = appUser.Surname;
            //model.Picture = appUser.Picture;
            //model.EMail = appUser.Email;
            //return View(model);
            return View(_mapper.Map<AppUserListViewDto>(appUser));
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserListViewDto model, IFormFile resim)
        {
            TempData["Active"] = MenuInfo.Profil;
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