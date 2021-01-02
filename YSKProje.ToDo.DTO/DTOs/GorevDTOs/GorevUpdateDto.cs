using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.GorevDTOs
{
    public class GorevUpdateDto
    {
        //GorevUpdateModel
        public int id { get; set; }

        //[Required(ErrorMessage = "Ad Alanı Boş Bırakılamaz..!")]
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        //[Range(0, int.MaxValue, ErrorMessage = "Lütfen Aciliyet Durumu Seçiniz..!")]
        public int AciliyetId { get; set; }
    }
}
