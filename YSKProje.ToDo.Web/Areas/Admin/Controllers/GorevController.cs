using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.GorevDTOs;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GorevController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly IAciliyetService _aciliyetService;
        private readonly IMapper _mapper;
        public GorevController(IGorevService gorevService,IAciliyetService aciliyetService, IMapper mapper)
        {
            _gorevService = gorevService;
            _aciliyetService = aciliyetService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            TempData["Active"] = "Gorev";
            //List<Gorev> gorevs= _gorevService.GetirAciliyetİleTamamlanmayan();
            //List<GorevListViewModel> gorevListViewModel = new List<GorevListViewModel>();
            //foreach (var item in gorevs)
            //{
            //    GorevListViewModel gorevListViewModel1 = new GorevListViewModel();
            //    gorevListViewModel1.Aciklama = item.Aciklama;
            //    gorevListViewModel1.Aciliyet = item.Aciliyet;
            //    gorevListViewModel1.AciliyetId = item.AciliyetId;
            //    gorevListViewModel1.Ad = item.Ad;
            //    gorevListViewModel1.Durum = item.Durum;
            //    gorevListViewModel1.Id  = item.Id;
            //    gorevListViewModel1.OlusturulmaTarih = item.OlusturulmaTarih;
            //    gorevListViewModel.Add(gorevListViewModel1);
            //}
            //return View(gorevListViewModel);

            return View(_mapper.Map<List<GorevListViewDto>>(_gorevService.GetirAciliyetİleTamamlanmayan()));
        }
        public IActionResult GorevVazgec()
        {
            return RedirectToAction("Index");
        }
        public IActionResult EkleGorev()
        {
            TempData["Active"] = "Gorev";
            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(),"Id","Tanim");
            return View(new GorevInsertDto());
        }
        [HttpPost]
        public IActionResult EkleGorev(GorevInsertDto gorevModel)
        {
            if (ModelState.IsValid)
            {
                _gorevService.Kaydet(new Gorev() {
                    Aciklama= gorevModel.Aciklama,
                    AciliyetId = gorevModel.AciliyetId,
                    Ad = gorevModel.Ad,
                });
                return RedirectToAction("Index");
            }
             
            return View(gorevModel);
        }
        public IActionResult GuncelleGorev(int id)
        {
            TempData["Active"] = "Gorev";
            var gorevler= _gorevService.GetirIdile(id);
            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim", gorevler.AciliyetId);
            //GorevUpdateModel gorevUpdateModel = new GorevUpdateModel()
            //{
            //    Aciklama = gorevler.Aciklama,
            //    AciliyetId = gorevler.AciliyetId,
            //    Ad = gorevler.Ad,
            //    id = gorevler.Id
            //};

            //return View(gorevUpdateModel);

            return View(_mapper.Map<GorevUpdateDto>(gorevler));  
        }
        [HttpPost]
        public IActionResult GuncelleGorev(GorevUpdateDto gorevModel)
        {
            if (ModelState.IsValid)
            {
                _gorevService.Guncelle(new Gorev()
                {
                    Id = gorevModel.id,
                    Aciklama = gorevModel.Aciklama,
                    AciliyetId = gorevModel.AciliyetId,
                    Ad = gorevModel.Ad,
                });
                return RedirectToAction("Index");
            }
            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim", gorevModel.AciliyetId);
            return View(gorevModel);
        }

        public IActionResult SilGorev (int id)
        {
            _gorevService.Sil(new Gorev()
            { 
                Id = id
            });

            return Json(null);
        }
    }
}
