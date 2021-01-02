using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.RaporDTOs;

namespace YSKProje.ToDo.Business.ValidationRules.FluentValidation
{
    public class RaporInsertValidator:AbstractValidator<RaporInsertViewDto>
    {
        public RaporInsertValidator()
        {
            RuleFor(x => x.Tanim).NotNull().WithMessage("Rapor Tanımı Boş Geçilemez.");
            RuleFor(x => x.Detay).NotNull().WithMessage("Rapor Detayı Boş Geçilemez.");

        } 
    }
}
