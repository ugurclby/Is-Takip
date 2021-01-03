using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.DTO.DTOs.GorevDTOs;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area(AreaInfo.Admin)]
    [Authorize(Roles = RoleInfo.Admin)]
    public class IsEmriController : BaseIdentityController
    {
        IAppUserService _appUserService;
        IGorevService _gorevService; 
        IDocumentService _documentService;
        IBildirimService _bildirimService;
        private readonly IMapper _mapper;
        public IsEmriController(IAppUserService appUserService, IGorevService gorevService, UserManager<AppUser> userManager, IDocumentService documentService, IBildirimService bildirimService, IMapper mapper):base(userManager)
        {
            _appUserService = appUserService;
            _gorevService = gorevService; 
            _documentService = documentService;
            _bildirimService = bildirimService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            TempData["Active"] = MenuInfo.IsEmri;

            List<Gorev> gorevler= _gorevService.GetirTumTablolarla();

            //List<GorevListAllViewModel> models = new List<GorevListAllViewModel>();

            //foreach (Gorev gorev in gorevler)
            //{
            //    GorevListAllViewModel gorevListAllViewModel = new GorevListAllViewModel();
            //    gorevListAllViewModel.Aciklama = gorev.Aciklama;
            //    gorevListAllViewModel.Ad = gorev.Ad;
            //    gorevListAllViewModel.Aciliyet = gorev.Aciliyet;
            //    gorevListAllViewModel.AppUser = gorev.AppUser;
            //    gorevListAllViewModel.Id = gorev.Id;
            //    gorevListAllViewModel.OlusturulmaTarih = gorev.OlusturulmaTarih;
            //    gorevListAllViewModel.Raporlar = gorev.Raporlar;
            //    models.Add(gorevListAllViewModel);
            //}
            ////var model = _appUserService.GetirAdminOlmayan();
            ////return View(models); 

            return View(_mapper.Map<List<GorevListAllViewDto>>(gorevler));
        }

        [HttpGet]
        public IActionResult AtaPersonel(int id,string s,int sayfa=1)
        {
            TempData["Active"] = MenuInfo.IsEmri; 
            ViewBag.AktifSayfa = sayfa;
            int toplamSayfa; 
            
            var model = _gorevService.GetirAciliyetileId(id); 
            var appUser = _appUserService.GetirAdminOlmayan(out toplamSayfa, s,sayfa);
            
            ViewBag.ToplamSayfa = toplamSayfa;
            ViewBag.ArananKelime = s;


            //List<AppUserListViewModel> appUserListViewModels = new List<AppUserListViewModel>();

            //foreach (var item in appUser)
            //{
            //    AppUserListViewModel appUserListViewModel = new AppUserListViewModel();
            //    appUserListViewModel.Id = item.Id;
            //    appUserListViewModel.Name = item.Name;
            //    appUserListViewModel.SurName = item.Surname;
            //    appUserListViewModel.Picture = item.Picture;
            //    appUserListViewModel.EMail = item.Email;
            //    appUserListViewModels.Add(appUserListViewModel);
            //}

            //ViewBag.Personeller = appUserListViewModels;

            ViewBag.Personeller = _mapper.Map<List<AppUserListViewDto>>(appUser);


            //GorevListViewModel gorevListViewModel = new GorevListViewModel()
            //{
            //    Aciklama = model.Aciklama,
            //    Aciliyet = model.Aciliyet,
            //    OlusturulmaTarih = model.OlusturulmaTarih,
            //    Ad = model.Ad,
            //    Durum = model.Durum,
            //    Id=model.Id
            //};
            //return View(gorevListViewModel);
            return View(_mapper.Map<GorevListViewDto>(model));
        }

        [HttpPost]
        public IActionResult AtaPersonel(PersonelGorevlendirDto personelGorevlendirModel)
        {
            TempData["Active"] = MenuInfo.IsEmri;
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

        public IActionResult PersonelGorevlendir(PersonelGorevlendirDto personelGorevlendirModel)
        {
            TempData["Active"] = MenuInfo.IsEmri;
            PersonelGorevListViewDto personelGorevListViewModel = new PersonelGorevListViewDto();

            var user= _userManager.Users.FirstOrDefault(x => x.Id == personelGorevlendirModel.PersonelId);
            var gorev = _gorevService.GetirAciliyetileId(personelGorevlendirModel.GorevId);

            //AppUserListViewDto appUserListViewModel = new AppUserListViewDto()
            //{
            //    EMail = user.Email,
            //    Id = user.Id,
            //    Name = user.Name,
            //    Picture = user.Picture,
            //    SurName = user.Surname
            //}; 

            //GorevListViewModel gorevListViewModel = new GorevListViewModel()
            //{
            //    Aciklama = gorev.Aciklama,
            //    Aciliyet = gorev.Aciliyet,
            //    AciliyetId = gorev.AciliyetId,
            //    Ad = gorev.Ad,
            //    Durum = gorev.Durum,
            //    Id = gorev.Id,
            //    OlusturulmaTarih = gorev.OlusturulmaTarih
            //};
            
            personelGorevListViewModel.user = _mapper.Map<AppUserListViewDto>(user);
            personelGorevListViewModel.gorev = _mapper.Map<GorevListViewDto>(gorev);

            return View(personelGorevListViewModel);
        }

        public IActionResult Gorevlendir(int id)
        {
            TempData["Active"] = MenuInfo.IsEmri;

            return View(GetGorevListAllViewModel(id));
        }

        public IActionResult ExcelAktar(int id)
        {
            TempData["Active"] = MenuInfo.IsEmri;
            var sonuc = GetGorevListAllViewModel(id);
            var excel= _documentService.ExcelAktar(sonuc.Raporlar);


            return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",Guid.NewGuid()+".xlsx");
        }

        public IActionResult PdfAktar(int id)
        {
            TempData["Active"] = MenuInfo.IsEmri;
            var sonuc = GetGorevListAllViewModel(id);
            var excel = _documentService.PdfAktar(sonuc.Raporlar);


            return File(excel, "application/pdf", Guid.NewGuid() + ".pdf");
        }

        public GorevListAllViewDto GetGorevListAllViewModel(int gorevId)
        {
            TempData["Active"] = MenuInfo.IsEmri;
            var sonuc = _gorevService.RaporGetirGorevIdile(gorevId);

            //GorevListAllViewModel gorevListAllViewModel = new GorevListAllViewModel()
            //{
            //    Id = sonuc.Id,
            //    Aciklama = sonuc.Aciklama,
            //    Aciliyet = sonuc.Aciliyet,
            //    Ad = sonuc.Ad,
            //    AppUser = sonuc.AppUser,
            //    OlusturulmaTarih = sonuc.OlusturulmaTarih,
            //    Raporlar = sonuc.Raporlar
            //};

            //return gorevListAllViewModel;
            return _mapper.Map<GorevListAllViewDto>(sonuc);
        }
    }
}
