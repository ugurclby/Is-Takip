using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = "Member")]
    [Area("Member")]
    public class HomeController : Controller
    {
        private readonly IRaporService _raporService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IGorevService _gorevService;
        private readonly IBildirimService _bildirimService;

        public HomeController(IRaporService raporService, UserManager<AppUser> userManager, IGorevService gorevService, IBildirimService bildirimService)
        {
            _raporService = raporService;
            _userManager = userManager;
            _gorevService = gorevService;
            _bildirimService = bildirimService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "Anasayfa";

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.RaporSayisi =  _raporService.GetirRaporSayisiileAppUserId(user.Id); 

            ViewBag.TamamlananGorevSayisi = _gorevService.TamamlananGorevSayisi(user.Id);
            
            ViewBag.TamamlanmasiGerekenGorevSayisi = _gorevService.TamamlanmasiGerekenGorevSayisi(user.Id);

            ViewBag.OkunmayanBildirimSayisi = _bildirimService.OkunmayanBildirimSayisi(user.Id);

            return View();
        }
    }
}
