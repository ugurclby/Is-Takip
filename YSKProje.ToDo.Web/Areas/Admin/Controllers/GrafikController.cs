using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area(AreaInfo.Admin)]
    [Authorize(Roles = RoleInfo.Admin)]
    public class GrafikController : Controller
    { 
        private readonly IAppUserService _appUserService;
        public GrafikController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        public IActionResult Index()
        {
            TempData["Active"] = MenuInfo.Grafik; 

            return View();
        }

        public IActionResult EnCokGorevTamamlamis()
        {
            TempData["Active"] = MenuInfo.Grafik;
            var sonuc = JsonConvert.SerializeObject(_appUserService.EnCokGorevTamamlamisPersonel());
            return Json(sonuc);
        }
        public IActionResult EnCokGorevdeCalisan()
        {
            TempData["Active"] = MenuInfo.Grafik;
            var sonuc = JsonConvert.SerializeObject(_appUserService.EnCokGorevdeCalisanPersonel());
            return Json(sonuc);
        }
    }
}
