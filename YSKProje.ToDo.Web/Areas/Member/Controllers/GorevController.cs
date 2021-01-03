using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.GorevDTOs;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Area(AreaInfo.Member)]
    [Authorize(Roles = RoleInfo.Member)]
    public class GorevController : BaseIdentityController
    {
        private readonly IGorevService _gorevService; 
        private readonly IMapper _mapper;
        public GorevController(IGorevService gorevService, UserManager<AppUser> userManager, IMapper mapper):base(userManager)
        {
            _gorevService = gorevService; 
            _mapper = mapper;
        }
        
        public  async Task<IActionResult> Index(int aktifsayfa=1)
        {
            TempData["Active"] = MenuInfo.Gorev;
            int toplamsayfa;
            var user = await GetirUserManager();
            var gorevler = _gorevService.GetirTumTablolarlaTamamlanmayan(out toplamsayfa, user.Id, aktifsayfa);
            ViewBag.ToplamSayfa = toplamsayfa;
            ViewBag.AktifSayfa = aktifsayfa;
            //List<GorevListAllViewModel> gorevListAllViewModels = new List<GorevListAllViewModel>();

            //foreach (var gorev in gorevler)
            //{
            //    GorevListAllViewModel gorevListAllViewModel = new GorevListAllViewModel();
            //    gorevListAllViewModel.Aciklama = gorev.Aciklama;
            //    gorevListAllViewModel.Aciliyet = gorev.Aciliyet;
            //    gorevListAllViewModel.Ad = gorev.Ad;
            //    gorevListAllViewModel.AppUser = gorev.AppUser;
            //    gorevListAllViewModel.Id = gorev.Id;
            //    gorevListAllViewModel.OlusturulmaTarih = gorev.OlusturulmaTarih;
            //    gorevListAllViewModel.Raporlar = gorev.Raporlar;
            //    gorevListAllViewModels.Add(gorevListAllViewModel);
            //} 
            //return View(gorevListAllViewModels);

            return View(_mapper.Map<List<GorevListAllViewDto>>(gorevler));
        }
    }
}
