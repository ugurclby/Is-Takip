using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Areas.Admin.Models
{
    public class PersonelGorevListViewModel
    {
        public GorevListViewModel gorev { get; set; }
        public AppUserListViewModel user { get; set; }
    }
}
