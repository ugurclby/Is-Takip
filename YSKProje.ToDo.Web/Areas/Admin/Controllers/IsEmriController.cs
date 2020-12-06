using Microsoft.AspNetCore.Authorization;
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
        public IsEmriController(IAppUserService appUserService, IGorevService gorevService)
        {
            _appUserService = appUserService;
            _gorevService = gorevService;
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
    }
}
