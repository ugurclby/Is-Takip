using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.AppUserDtos
{
    public class AppUserViewDto
    {
        //AppUserViewModel
        //[Display(Name = "Kullanıcı Adı :")]
        //[Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez")]
        public string UserName { get; set; }

        //[Display(Name = "Parola :")]
        //[DataType(DataType.Password)]
        //[Required(ErrorMessage = "Parola Boş Geçilemez")]
        public string Password { get; set; }

        //[Display(Name = "Parolanızı Tekrar Giriniz :")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Parolalar Eşleşmiyor")]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "Email :")]
        //[EmailAddress(ErrorMessage = "Geçersiz Mail")]
        //[Required(ErrorMessage = "EMail Boş Geçilemez")]
        public string Email { get; set; }

        //[Display(Name = "Adınız :")]
        //[Required(ErrorMessage = "Ad Boş Geçilemez")]
        public string Name { get; set; }

        //[Display(Name = "Soyadınız :")]
        //[Required(ErrorMessage = "Soyad Boş Geçilemez")]
        public string SurName { get; set; }
    }
}
