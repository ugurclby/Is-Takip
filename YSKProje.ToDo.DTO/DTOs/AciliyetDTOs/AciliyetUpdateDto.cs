using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.AciliyetDTOs
{
    public class AciliyetUpdateDto
    {
        //AciliyetUpdateModel
        public int id { get; set; }
        //[Display(Name = "Tanım :")]
        //[Required(ErrorMessage = "Tanım Boş Bırakılamaz..!")]
        public string Tanim { get; set; }
    }
}
