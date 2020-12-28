using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GrafikController : Controller
    { 
        private readonly IAppUserService _appUserService;
        public GrafikController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        public IActionResult Index()
        {
            TempData["Active"] = "Grafik"; 

            return View();
        }

        public IActionResult EnCokGorevTamamlamis()
        {
            var sonuc = JsonConvert.SerializeObject(_appUserService.EnCokGorevTamamlamisPersonel());
            return Json(sonuc);
        }
        public IActionResult EnCokGorevdeCalisan()
        {
            var sonuc = JsonConvert.SerializeObject(_appUserService.EnCokGorevdeCalisanPersonel());
            return Json(sonuc);
        }
    }
}
