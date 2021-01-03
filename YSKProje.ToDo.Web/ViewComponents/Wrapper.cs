using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.ViewComponents
{
    public class Wrapper : ViewComponent
    {
        // ViewComponentler e ait view'ler shared altında Components altında class ismi  bir klasör daha oluşturulur ve default isminde view oluşturulur.
        private readonly UserManager<AppUser> _userManager;
        private readonly IBildirimService _bildirimService;
        private readonly IMapper _mapper;
        public Wrapper(UserManager<AppUser> userManager, IBildirimService bildirimService, IMapper mapper)
        {
            _userManager = userManager;
            _bildirimService = bildirimService;
            _mapper = mapper;
        }
        public IViewComponentResult Invoke()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result; //async metod olduğu için await olması gerekirdi. Fakat IViewComponentResult task dönemez. o yüzden result ile dönüldü.
            //AppUserListViewDto appUserListViewModel = new AppUserListViewDto()
            //{
            //    EMail = user.Email,
            //    Id = user.Id,
            //    Name = user.Name,
            //    SurName = user.Surname,
            //    Picture = user.Picture
            //}; 
            int okunmamisBildirim = _bildirimService.OkunmamisBildirim(user.Id).Count();

            ViewBag.OkunmamisBildirim = okunmamisBildirim;

            var roles = _userManager.GetRolesAsync(user).Result;

            if (roles.Contains("Admin"))
            {
                return View(_mapper.Map<AppUserListViewDto>(user));
            }
            return View("Member", _mapper.Map<AppUserListViewDto>(user));
            
        }
    }
}
