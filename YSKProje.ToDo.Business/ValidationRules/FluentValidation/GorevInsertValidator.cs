﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.GorevDTOs;

namespace YSKProje.ToDo.Business.ValidationRules.FluentValidation
{
    public class GorevInsertValidator:AbstractValidator<GorevInsertDto>
    {
        public GorevInsertValidator()
        {
            RuleFor(x => x.Ad).NotNull().WithMessage("Ad Alanı Boş Bırakılamaz.");
            RuleFor(x => x.AciliyetId).ExclusiveBetween(0,int.MaxValue).WithMessage("Aciliyet Seçiniz.");
        }
    }
}
