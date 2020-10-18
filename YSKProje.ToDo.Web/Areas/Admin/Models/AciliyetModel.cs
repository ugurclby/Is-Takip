using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Areas.Admin.Models
{
    public class AciliyetModel
    {
        [Display(Name ="Tanım")]
        [Required(ErrorMessage = "Aciliyet Tanımı Boş Geçilemez")]
        public string Tanim { get; set; }
    }
}
