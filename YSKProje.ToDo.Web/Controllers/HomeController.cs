using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;

namespace YSKProje.ToDo.Web.Controllers
{
    public class HomeController : Controller
    {
        IGorevService _gorevService;
        public HomeController(IGorevService gorevService)
        {
            _gorevService = gorevService; 
        }
        public IActionResult Index()
        {
            return View(); 
        }
        public IActionResult KayitOl()
        {
            return View();
        }

    }
}
