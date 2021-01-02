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

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class BildirimController : Controller
    {
        private readonly IBildirimService _bildirimService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public BildirimController(IBildirimService bildirimService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _bildirimService = bildirimService;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "Bildirim";

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            //var bildirimList = _bildirimService.OkunmamisBildirim(user.Id);

            //List<OkunmamisBildirimListViewModel> bildirimler = new List<OkunmamisBildirimListViewModel>();

            //foreach (var item in bildirimList)
            //{
            //    OkunmamisBildirimListViewModel bildirim = new OkunmamisBildirimListViewModel() {
            //        Aciklama = item.Aciklama,
            //        Id = item.Id 
            //    };

            //    bildirimler.Add(bildirim);
            //}

            //return View(bildirimler);

            return View(_mapper.Map<List<BildirimListViewDto>>(_bildirimService.OkunmamisBildirim(user.Id)));
        }
        [HttpPost]
        public IActionResult Index(int bildirimId)
        {
            TempData["Active"] = "Bildirim";
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
