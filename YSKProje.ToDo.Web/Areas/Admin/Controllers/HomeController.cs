using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area(AreaInfo.Admin)]
    [Authorize(Roles = RoleInfo.Admin)]
    public class HomeController : BaseIdentityController
    {
        private readonly IGorevService _gorevService;
        private readonly IBildirimService _bildirimService; 
        private readonly IRaporService _raporService;

        public HomeController(IGorevService gorevService, IBildirimService bildirimService, UserManager<AppUser> userManager, IRaporService raporService):base(userManager)
        {
            _gorevService = gorevService;
            _bildirimService = bildirimService; 
            _raporService = raporService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = MenuInfo.Anasayfa;
            var user = await GetirUserManager();
            ViewBag.AtanmayiBekleyenGorev = _gorevService.AtanmayiBekleyenGorevSayisi();
            ViewBag.TamamlananGorevSayisi = _gorevService.TamamlananGorevSayisi();
            ViewBag.OkunmayanBildirimSayisi = _bildirimService.OkunmayanBildirimSayisi(user.Id);
            ViewBag.ToplamYazilanRaporSayisi = _raporService.ToplamYazilanRaporSayisi();
            return View();
        }
    }
}
