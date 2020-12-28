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

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class IsEmriController : Controller
    {
        IAppUserService _appUserService;
        IGorevService _gorevService;
        UserManager<AppUser> _userManager;
        IDocumentService _documentService;
        IBildirimService _bildirimService;
        public IsEmriController(IAppUserService appUserService, IGorevService gorevService, UserManager<AppUser> userManager, IDocumentService documentService, IBildirimService bildirimService)
        {
            _appUserService = appUserService;
            _gorevService = gorevService;
            _userManager = userManager;
            _documentService = documentService;
            _bildirimService = bildirimService;
        }
        public IActionResult Index()
        {
            TempData["Active"] = "IsEmri";

            List<Gorev> gorevler= _gorevService.GetirTumTablolarla();

            List<GorevListAllViewModel> models = new List<GorevListAllViewModel>();

            foreach (Gorev gorev in gorevler)
            {
                GorevListAllViewModel gorevListAllViewModel = new GorevListAllViewModel();
                gorevListAllViewModel.Aciklama = gorev.Aciklama;
                gorevListAllViewModel.Ad = gorev.Ad;
                gorevListAllViewModel.Aciliyet = gorev.Aciliyet;
                gorevListAllViewModel.AppUser = gorev.AppUser;
                gorevListAllViewModel.Id = gorev.Id;
                gorevListAllViewModel.OlusturulmaTarih = gorev.OlusturulmaTarih;
                gorevListAllViewModel.Raporlar = gorev.Raporlar;
                models.Add(gorevListAllViewModel);
            }
            //var model = _appUserService.GetirAdminOlmayan();
            return View(models);
        }

        [HttpGet]
        public IActionResult AtaPersonel(int id,string s,int sayfa=1)
        {
            TempData["Active"] = "IsEmri";
            ViewBag.AktifSayfa = sayfa;
            int toplamSayfa;
            //ViewBag.ToplamSayfa = (int)Math.Ceiling((double)_appUserService.GetirAdminOlmayan().Count() / 3);
            
            var model = _gorevService.GetirAciliyetileId(id); 
            var appUser = _appUserService.GetirAdminOlmayan(out toplamSayfa, s,sayfa);
            ViewBag.ToplamSayfa = toplamSayfa;
            ViewBag.ArananKelime = s;
            List<AppUserListViewModel> appUserListViewModels = new List<AppUserListViewModel>();

            foreach (var item in appUser)
            {
                AppUserListViewModel appUserListViewModel = new AppUserListViewModel();
                appUserListViewModel.Id = item.Id;
                appUserListViewModel.Name = item.Name;
                appUserListViewModel.SurName = item.Surname;
                appUserListViewModel.Picture = item.Picture;
                appUserListViewModel.EMail = item.Email;
                appUserListViewModels.Add(appUserListViewModel);
            }

            ViewBag.Personeller = appUserListViewModels;

            GorevListViewModel gorevListViewModel = new GorevListViewModel()
            {
                Aciklama = model.Aciklama,
                Aciliyet = model.Aciliyet,
                OlusturulmaTarih = model.OlusturulmaTarih,
                Ad = model.Ad,
                Durum = model.Durum,
                Id=model.Id
            };
            return View(gorevListViewModel);
        }

        [HttpPost]
        public IActionResult AtaPersonel(PersonelGorevlendirModel personelGorevlendirModel)
        {
            TempData["Active"] = "IsEmri";
            var guncellenecekGorev= _gorevService.GetirIdile(personelGorevlendirModel.GorevId);
            guncellenecekGorev.AppUserId = personelGorevlendirModel.PersonelId;
            _gorevService.Guncelle(guncellenecekGorev);

            _bildirimService.Kaydet(new Bildirim()
            {
                AppUserId = personelGorevlendirModel.PersonelId,
                Aciklama = $"{guncellenecekGorev.Ad} adlı iş için görevlendirildiniz."
            });

            return RedirectToAction("Index");
        }

        public IActionResult PersonelGorevlendir(PersonelGorevlendirModel personelGorevlendirModel)
        {
            TempData["Active"] = "IsEmri";
            PersonelGorevListViewModel personelGorevListViewModel = new PersonelGorevListViewModel();

            var user= _userManager.Users.FirstOrDefault(x => x.Id == personelGorevlendirModel.PersonelId);
            var gorev = _gorevService.GetirAciliyetileId(personelGorevlendirModel.GorevId);

            AppUserListViewModel appUserListViewModel = new AppUserListViewModel()
            {
                EMail = user.Email,
                Id = user.Id,
                Name = user.Name,
                Picture = user.Picture,
                SurName = user.Surname
            };

            GorevListViewModel gorevListViewModel = new GorevListViewModel()
            {
                Aciklama = gorev.Aciklama,
                Aciliyet = gorev.Aciliyet,
                AciliyetId = gorev.AciliyetId,
                Ad = gorev.Ad,
                Durum = gorev.Durum,
                Id = gorev.Id,
                OlusturulmaTarih = gorev.OlusturulmaTarih
            };
            
            personelGorevListViewModel.user = appUserListViewModel;
            personelGorevListViewModel.gorev = gorevListViewModel; 

            return View(personelGorevListViewModel);
        }

        public IActionResult Gorevlendir(int id)
        {
            TempData["Active"] = "IsEmri"; 
            
            return View(GetGorevListAllViewModel(id));
        }

        public IActionResult ExcelAktar(int id)
        {
            var sonuc = GetGorevListAllViewModel(id);
            var excel= _documentService.ExcelAktar(sonuc.Raporlar);


            return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",Guid.NewGuid()+".xlsx");
        }

        public IActionResult PdfAktar(int id)
        {
            var sonuc = GetGorevListAllViewModel(id);
            var excel = _documentService.PdfAktar(sonuc.Raporlar);


            return File(excel, "application/pdf", Guid.NewGuid() + ".pdf");
        }

        public GorevListAllViewModel GetGorevListAllViewModel(int gorevId)
        {  
            var sonuc = _gorevService.RaporGetirGorevIdile(gorevId);

            GorevListAllViewModel gorevListAllViewModel = new GorevListAllViewModel()
            {
                Id = sonuc.Id,
                Aciklama = sonuc.Aciklama,
                Aciliyet = sonuc.Aciliyet,
                Ad = sonuc.Ad,
                AppUser = sonuc.AppUser,
                OlusturulmaTarih = sonuc.OlusturulmaTarih,
                Raporlar = sonuc.Raporlar
            };
             
            return gorevListAllViewModel;
        }
    }
}
