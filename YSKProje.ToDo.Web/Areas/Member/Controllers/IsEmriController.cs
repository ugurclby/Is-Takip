using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;
using YSKProje.ToDo.Web.Areas.Member.Models;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles ="Member")]
    [Area("Member")]
    public class IsEmriController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRaporService _raporService;
        private readonly IBildirimService _bildirimService;

        public IsEmriController(IGorevService gorevService, UserManager<AppUser> userManager, IRaporService raporService, IBildirimService bildirimService)
        {
            _gorevService = gorevService;
            _userManager = userManager;
            _raporService = raporService;
            _bildirimService = bildirimService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "IsEmri";
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var gorevler = _gorevService.GetirTumTablolarla(x => x.AppUserId == user.Id && !x.Durum);

            List<GorevListAllViewModel> gorevListAllViewModels = new List<GorevListAllViewModel>();

            foreach (var item in gorevler)
            {
                GorevListAllViewModel gorevListAllView = new GorevListAllViewModel();

                gorevListAllView.Ad = item.Ad;
                gorevListAllView.Aciklama = item.Aciklama;
                gorevListAllView.Aciliyet = item.Aciliyet;
                gorevListAllView.AppUser = item.AppUser;
                gorevListAllView.Id = item.Id;
                gorevListAllView.OlusturulmaTarih = item.OlusturulmaTarih;
                gorevListAllView.Raporlar = item.Raporlar;
                gorevListAllViewModels.Add(gorevListAllView);
            }

            return View(gorevListAllViewModels);
        }
        public IActionResult RaporEkle (int id)
        {
            TempData["Active"] = "IsEmri";

            var gorev = _gorevService.GetirAciliyetileId(id);

            RaporAddViewModel raporAddViewModel = new RaporAddViewModel();

            raporAddViewModel.GorevId = id;
            raporAddViewModel.Gorev = gorev;

            return View(raporAddViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> RaporEkle(RaporAddViewModel raporAddViewModel)
        {
            TempData["Active"] = "IsEmri";
            if (ModelState.IsValid)
            {
                Rapor rapor = new Rapor();
                 
                rapor.Tanim = raporAddViewModel.Tanim;
                rapor.Detay = raporAddViewModel.Detay;
                rapor.GorevId = raporAddViewModel.GorevId; 

                _raporService.Kaydet(rapor);
              
                var adminRole = await _userManager.GetUsersInRoleAsync("Admin");

                var aktifKullanici = await _userManager.FindByNameAsync(User.Identity.Name);

                foreach (var admin in adminRole)
                {
                    _bildirimService.Kaydet(new Bildirim()
                    {
                        AppUserId = admin.Id,
                        Aciklama = $"{aktifKullanici.Name} {aktifKullanici.Surname} isimli kullanıcı rapor ekledi."
                    });
                }


                return RedirectToAction("Index");
            } 

            return View(raporAddViewModel);
        }

        public IActionResult RaporDuzenle(int id)
        {
            TempData["Active"] = "IsEmri";

            var rapor = _raporService.RaporGetirGorevileId(id);


            //var rapor = _raporService.GetirIdile(id); 

            RaporAddViewModel raporAddViewModel = new RaporAddViewModel();

            raporAddViewModel.GorevId = rapor.GorevId;
            raporAddViewModel.Detay = rapor.Detay;
            raporAddViewModel.Tanim = rapor.Tanim;
            raporAddViewModel.Id = id;
            raporAddViewModel.Gorev = rapor.Gorev;


            return View(raporAddViewModel);
        }
        [HttpPost]
        public IActionResult RaporDuzenle(RaporAddViewModel raporAddViewModel)
        {
            TempData["Active"] = "IsEmri";  
            
            if (ModelState.IsValid)
            {
                Rapor rapor = new Rapor();

                rapor.Tanim = raporAddViewModel.Tanim;
                rapor.Detay = raporAddViewModel.Detay;
                rapor.GorevId = raporAddViewModel.GorevId;
                rapor.Id = raporAddViewModel.Id;

                _raporService.Guncelle(rapor);
                
                return RedirectToAction("Index");
            }

            return View(raporAddViewModel); 
        }

        public IActionResult SilRapor (int RaporId)
        {
            _raporService.Sil(new Rapor()
            {
                Id = RaporId
            });

            return Json(null);
        }

        public async Task<IActionResult> GorevTamamla (int GorevId)
        {
            var gorev = _gorevService.GetirIdile(GorevId);
            gorev.Durum = true; 
            _gorevService.Guncelle(gorev);

            var adminRole = await _userManager.GetUsersInRoleAsync("Admin");

            var aktifKullanici = await _userManager.FindByNameAsync(User.Identity.Name);

            foreach (var admin in adminRole)
            {
                _bildirimService.Kaydet(new Bildirim()
                {
                    AppUserId = admin.Id,
                    Aciklama = $"{aktifKullanici.Name} {aktifKullanici.Surname} isimli kullanıcı görevi tamamladı."
                });
            }
            return Json(null);
        }
    }
}
