using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;

namespace YSKProje.ToDo.Business.ValidationRules.FluentValidation
{
    public class AppUserListViewValidator:AbstractValidator<AppUserListViewDto>
    {
        public AppUserListViewValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Ad Alanı Boş Geçilemez..!");
            RuleFor(x => x.SurName).NotNull().WithMessage("Soyad Alanı Boş Geçilemez..!");

        } 
    }
}
