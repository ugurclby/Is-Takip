using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.ViewComponents
{
    public class Wrapper : ViewComponent
    {
        // ViewComponentler e ait view'ler shared altında Components altında class ismi  bir klasör daha oluşturulur ve default isminde view oluşturulur.
        private readonly UserManager<AppUser> _userManager;
        public Wrapper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IViewComponentResult Invoke()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result; //async metod olduğu için await olması gerekirdi. Fakat IViewComponentResult task dönemez. o yüzden result ile dönüldü.
            AppUserListViewModel appUserListViewModel = new AppUserListViewModel()
            {
                EMail = user.Email,
                Id = user.Id,
                Name = user.Name,
                SurName = user.Surname,
                Picture = user.Picture
            };

            var roles = _userManager.GetRolesAsync(user).Result;

            if (roles.Contains("Admin"))
            {
                return View(appUserListViewModel);
            }
            return View("Member", appUserListViewModel);
            
        }
    }
}
