using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;

namespace YSKProje.ToDo.Business.ValidationRules.FluentValidation
{
   public class AppUserViewValidator:AbstractValidator<AppUserViewDto>
    {
        public AppUserViewValidator()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage("Kullanıcı Adı Boş Geçilemez");
            RuleFor(x => x.Password).NotNull().WithMessage("Parola Boş Geçilemez");
            RuleFor(x => x.ConfirmPassword).NotNull().WithMessage("Parola Onay Boş Geçilemez");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Parolalar Eşleşmiyor");
            RuleFor(x => x.Email).NotNull().WithMessage("Email Boş Geçilemez").EmailAddress().WithMessage("Geçerli Email Adresi giriniz");
            RuleFor(x => x.Name).NotNull().WithMessage("Ad Boş Geçilemez"); 
            RuleFor(x => x.SurName).NotNull().WithMessage("Soyadı Boş Geçilemez"); 
        }

 
    }
}
