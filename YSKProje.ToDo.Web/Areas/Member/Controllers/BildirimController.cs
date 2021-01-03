using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.BildirimDTOs;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Area(AreaInfo.Member)]
    [Authorize(Roles = RoleInfo.Member)]
    public class BildirimController : BaseIdentityController
    {
        private readonly IBildirimService _bildirimService; 
        private readonly IMapper _mapper;
        public BildirimController(IBildirimService bildirimService, UserManager<AppUser> userManager, IMapper mapper):base(userManager)
        {
            _bildirimService = bildirimService; 
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = MenuInfo.Bildirim;

            var user = await GetirUserManager();

            var bildirimList = _bildirimService.OkunmamisBildirim(user.Id);

            //List<OkunmamisBildirimListViewModel> bildirimler = new List<OkunmamisBildirimListViewModel>();

            //foreach (var item in bildirimList)
            //{
            //    OkunmamisBildirimListViewModel bildirim = new OkunmamisBildirimListViewModel()
            //    {
            //        Aciklama = item.Aciklama,
            //        Id = item.Id
            //    };

            //    bildirimler.Add(bildirim);
            //}

            //return View(bildirimler);
            return View(_mapper.Map<List<BildirimListViewDto>>(bildirimList));
        }
        [HttpPost]
        public IActionResult Index(int bildirimId)
        {
            TempData["Active"] = MenuInfo.Bildirim;
            if (ModelState.IsValid)
            {
                var bildirim = _bildirimService.GetirIdile(bildirimId);
                bildirim.Durum = true;

                _bildirimService.Guncelle(bildirim);
            }
            return RedirectToAction("Index");
        }
    }
}
