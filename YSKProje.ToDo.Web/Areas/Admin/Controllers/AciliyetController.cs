using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.AciliyetDTOs;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AciliyetController : Controller
    {
        private readonly IAciliyetService _aciliyetService;
        private readonly IMapper _mapper;

        public AciliyetController(IAciliyetService aciliyetService, IMapper mapper)
        {
            _aciliyetService = aciliyetService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            TempData["Active"] = "Aciliyet";
            //List<Aciliyet> aciliyets = _aciliyetService.GetirHepsi();
            //List<AciliyetListViewModel> aciliyetListViewModel = new List<AciliyetListViewModel>();

            //foreach (var item in aciliyets)
            //{
            //     AciliyetListViewModel  _aciliyetListViewModel = new  AciliyetListViewModel();
            //    _aciliyetListViewModel.Id = item.Id;
            //    _aciliyetListViewModel.Tanim = item.Tanim;
            //    aciliyetListViewModel.Add(_aciliyetListViewModel);
            //}
            //return View(aciliyetListViewModel);
            

            return View(_mapper.Map<List<AciliyetListViewDto>>(_aciliyetService.GetirHepsi()));
        }
        public IActionResult AciliyetVazgec()
        {
            return RedirectToAction("Index");
        }
        public IActionResult AciliyetEkle ()
        {
            TempData["Active"] = "Aciliyet";
            return View(new AciliyetInsertDto());
        }
        [HttpPost]
        public IActionResult AciliyetEkle(AciliyetInsertDto aciliyetModel)
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
            //Aciliyet aciliyet = _aciliyetService.GetirIdile(id);
            //AciliyetUpdateModel aciliyetUpdateModel = new AciliyetUpdateModel()
            //{
            //    id = aciliyet.Id,
            //    Tanim = aciliyet.Tanim
            //}; 

            //return View(aciliyetUpdateModel);
            return View(_mapper.Map<AciliyetUpdateDto>(_aciliyetService.GetirIdile(id)));
        }
        [HttpPost]
        public IActionResult GuncelleAciliyet(AciliyetUpdateDto aciliyetUpdateModel)
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
