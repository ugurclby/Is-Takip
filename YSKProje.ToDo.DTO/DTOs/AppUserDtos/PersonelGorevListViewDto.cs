using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.GorevDTOs;

namespace YSKProje.ToDo.DTO.DTOs.AppUserDtos
{
    public class PersonelGorevListViewDto
    {
        //PersonelGorevListViewModel
        public GorevListViewDto gorev { get; set; }
        public AppUserListViewDto user { get; set; }
    }
}
