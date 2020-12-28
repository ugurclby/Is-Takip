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
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class BildirimController : Controller
    {
        private readonly IBildirimService _bildirimService;
        private readonly UserManager<AppUser> _userManager;
        public BildirimController(IBildirimService bildirimService, UserManager<AppUser> userManager)
        {
            _bildirimService = bildirimService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "Bildirim";

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var bildirimList = _bildirimService.OkunmamisBildirim(user.Id);

            List<OkunmamisBildirimListViewModel> bildirimler = new List<OkunmamisBildirimListViewModel>();

            foreach (var item in bildirimList)
            {
                OkunmamisBildirimListViewModel bildirim = new OkunmamisBildirimListViewModel()
                {
                    Aciklama = item.Aciklama,
                    Id = item.Id
                };

                bildirimler.Add(bildirim);
            }

            return View(bildirimler);
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
