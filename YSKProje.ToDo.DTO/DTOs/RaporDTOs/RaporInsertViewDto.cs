using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.RaporDTOs
{
    public class RaporInsertViewDto
    {
        //RaporAddViewModel
        public int Id { get; set; }
        //[Display(Name = "Tanım")]
        //[Required(ErrorMessage = "Rapor Tanımı Boş Geçilemez")]
        public string Tanim { get; set; }
        //[Display(Name = "Detay")]
        //[Required(ErrorMessage = "Rapor Detayı Boş Geçilemez")]
        public string Detay { get; set; }

        public int GorevId { get; set; }
        //public Gorev Gorev { get; set; }
    }
}
