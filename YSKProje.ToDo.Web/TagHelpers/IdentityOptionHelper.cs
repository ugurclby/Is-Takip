using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.TagHelpers
{
    public class IdentityOptionHelper
    { 

        private static void IdentityAyarlar(IdentityOptions obj)
        {
            obj.Password.RequireDigit = false;
            obj.Password.RequiredLength = 1;
            obj.Password.RequireUppercase = false;
            obj.Password.RequireLowercase = false;
            obj.Password.RequireNonAlphanumeric = false;
        }
        private void CookieAyarlar(CookieAuthenticationOptions obj)
        {
            obj.Cookie.Name = "IsTakipCookie";
            obj.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
            obj.Cookie.HttpOnly = true;
            obj.ExpireTimeSpan = TimeSpan.FromDays(20);
            obj.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
            obj.LoginPath = "/Home/Index";
        }
        public Action<IdentityOptions> Action()
        {
            Action<IdentityOptions> action = new Action<IdentityOptions>(IdentityAyarlar);

            return action;
        }
        public Action<CookieAuthenticationOptions> CookieAction()
        {
            Action<CookieAuthenticationOptions> action = new Action<CookieAuthenticationOptions>(CookieAyarlar);

            return action;
        }


    }
}
