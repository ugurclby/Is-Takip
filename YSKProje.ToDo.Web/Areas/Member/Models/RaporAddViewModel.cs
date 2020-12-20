using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.Areas.Member.Models
{
    public class RaporAddViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Tanım")]
        [Required(ErrorMessage = "Rapor Tanımı Boş Geçilemez")]
        public string Tanim { get; set; }
        [Display(Name = "Detay")]
        [Required(ErrorMessage = "Rapor Detayı Boş Geçilemez")]
        public string Detay { get; set; }

        public int GorevId { get; set; }
        public Gorev Gorev { get; set; }


    }
}
