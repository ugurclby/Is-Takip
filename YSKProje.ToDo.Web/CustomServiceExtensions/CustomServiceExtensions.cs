using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.ValidationRules.FluentValidation;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DTO.DTOs.AciliyetDTOs;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.DTO.DTOs.GorevDTOs;
using YSKProje.ToDo.DTO.DTOs.RaporDTOs;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.CustomServiceExtensions
{
    public static class CustomServiceExtensions
    {
        public static void AddCustomIdentityExtension (this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(x => {
                x.Password.RequireDigit = false;
                x.Password.RequiredLength = 1;
                x.Password.RequireUppercase = false;
                x.Password.RequireLowercase = false;
                x.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<TodoContext>();
        }
        public static void AddCustomCookieExtension(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(x => {
                x.Cookie.Name = "IsTakipCookie";
                x.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                x.Cookie.HttpOnly = true;
                x.ExpireTimeSpan = TimeSpan.FromDays(20);
                x.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
                x.LoginPath = "/Home/Index";
            });
        }

        public static void AddCustomValidateContainer(this IServiceCollection services)
        {
            services.AddTransient<IValidator<AciliyetInsertDto>, AciliyetInsertValidator>();
            services.AddTransient<IValidator<AciliyetUpdateDto>, AciliyetUpdateValidator>();
            services.AddTransient<IValidator<AppUserListViewDto>, AppUserListViewValidator>();
            services.AddTransient<IValidator<AppUserSignInDto>, AppUserSignInValidator>();
            services.AddTransient<IValidator<AppUserViewDto>, AppUserViewValidator>();
            services.AddTransient<IValidator<GorevInsertDto>, GorevInsertValidator>();
            services.AddTransient<IValidator<RaporInsertViewDto>, RaporInsertValidator>();
        }
    }
}
