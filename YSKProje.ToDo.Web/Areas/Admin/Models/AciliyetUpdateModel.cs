using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Areas.Admin.Models
{
    public class AciliyetUpdateModel
    {
        public int id { get; set; }
        [Display(Name ="Tanım :")]
        [Required(ErrorMessage ="Tanım Boş Bırakılamaz..!")]
        public string Tanim { get; set; }
    }
}
 