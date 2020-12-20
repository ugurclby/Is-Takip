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

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = "Member")]
    [Area("Member")]
    
    public class GorevController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly UserManager<AppUser> _userManager;
        public GorevController(IGorevService gorevService, UserManager<AppUser> userManager)
        {
            _gorevService = gorevService;
            _userManager = userManager;
        }
        
        public  async Task<IActionResult> Index(int aktifsayfa=1)
        {
            TempData["Active"] = "Gorev";
            int toplamsayfa;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var gorevler = _gorevService.GetirTumTablolarlaTamamlanmayan(out toplamsayfa, user.Id, aktifsayfa);
            ViewBag.ToplamSayfa = toplamsayfa;
            ViewBag.AktifSayfa = aktifsayfa;
            List<GorevListAllViewModel> gorevListAllViewModels = new List<GorevListAllViewModel>();

            foreach (var gorev in gorevler)
            {
                GorevListAllViewModel gorevListAllViewModel = new GorevListAllViewModel();
                gorevListAllViewModel.Aciklama = gorev.Aciklama;
                gorevListAllViewModel.Aciliyet = gorev.Aciliyet;
                gorevListAllViewModel.Ad = gorev.Ad;
                gorevListAllViewModel.AppUser = gorev.AppUser;
                gorevListAllViewModel.Id = gorev.Id;
                gorevListAllViewModel.OlusturulmaTarih = gorev.OlusturulmaTarih;
                gorevListAllViewModel.Raporlar = gorev.Raporlar;
                gorevListAllViewModels.Add(gorevListAllViewModel);
            } 
            return View(gorevListAllViewModels);
        }
    }
}
