using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AciliyetController : Controller
    {
        private readonly IAciliyetService _aciliyetService;
        public AciliyetController(IAciliyetService aciliyetService)
        {
            _aciliyetService = aciliyetService;
        }
        public IActionResult Index()
        {
            TempData["Active"] = "Aciliyet";
            List<Aciliyet> aciliyets = _aciliyetService.GetirHepsi();
            List<AciliyetListViewModel> aciliyetListViewModel = new List<AciliyetListViewModel>();

            foreach (var item in aciliyets)
            {
                 AciliyetListViewModel  _aciliyetListViewModel = new  AciliyetListViewModel();
                _aciliyetListViewModel.Id = item.Id;
                _aciliyetListViewModel.Tanim = item.Tanim;
                aciliyetListViewModel.Add(_aciliyetListViewModel);
            }

            return View(aciliyetListViewModel);
        }
        public IActionResult AciliyetEkle ()
        {
            TempData["Active"] = "Aciliyet";
            return View(new AciliyetModel());
        }
        [HttpPost]
        public IActionResult AciliyetEkle(AciliyetModel aciliyetModel)
        {
            if (ModelState.IsValid)
            {
                _aciliyetService.Kaydet(new Aciliyet()
                {
                    Tanim = aciliyetModel.Tanim
                });
                return RedirectToAction("Index");
            }
            return View(aciliyetModel);
        }

        public IActionResult GuncelleAciliyet(int id)
        {
            TempData["Active"] = "Aciliyet";
            Aciliyet aciliyet = _aciliyetService.GetirIdile(id);
            AciliyetUpdateModel aciliyetUpdateModel = new AciliyetUpdateModel()
            {
                id = aciliyet.Id,
                Tanim = aciliyet.Tanim
            }; 

            return View(aciliyetUpdateModel);
        }
        [HttpPost]
        public IActionResult GuncelleAciliyet(AciliyetUpdateModel aciliyetUpdateModel)
        {
            if (ModelState.IsValid)
            {
                _aciliyetService.Guncelle(new Aciliyet()
                {   
                    Id= aciliyetUpdateModel.id,
                    Tanim = aciliyetUpdateModel.Tanim
                });
                return RedirectToAction("Index");
            }
            return View(aciliyetUpdateModel);   
        }
    }
}
