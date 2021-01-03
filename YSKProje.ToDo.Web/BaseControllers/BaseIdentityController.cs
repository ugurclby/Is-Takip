using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.BaseControllers
{
    public class BaseIdentityController : Controller
    {
        protected UserManager<AppUser> _userManager; // protected tanımlandığı zaman kalıtıldığı class içinde bu değişken kullanılabilir.
        protected BaseIdentityController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        protected async Task<AppUser> GetirUserManager()
        {
            return await _userManager.FindByNameAsync(User.Identity.Name);
        }
    }
}
