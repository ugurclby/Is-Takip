using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.AppUserDtos
{
    public class AppUserListViewDto
    {
        //AppUserListViewModel
        public int Id { get; set; }
        //[Required(ErrorMessage = "Ad Alanı Boş Geçilemez..!")]
        //[Display(Name = "Ad: ")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Soyad Alanı Boş Geçilemez..!")]
        //[Display(Name = "Soyad: ")]
        public string SurName { get; set; }
        //[Display(Name = "Email: ")]
        public string EMail { get; set; }
        //[Display(Name = "Resim: ")]
        public string Picture { get; set; }
    }
}
