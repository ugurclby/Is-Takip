using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.AciliyetDTOs;

namespace YSKProje.ToDo.Business.ValidationRules.FluentValidation
{
    public class AciliyetInsertValidator:AbstractValidator<AciliyetInsertDto>
    {
        public AciliyetInsertValidator()
        {
            RuleFor(x => x.Tanim).NotNull().WithMessage("Aciliyet Tanımı Boş Geçilemez");
        }
    }
}
