using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Interfaces
{
    public interface IAppUserDal:IGenericDal<AppUser>
    {
        List<AppUser> GetirAdminOlmayan();
        List<AppUser> GetirAdminOlmayan(out int toplamSayfa, string aranacakKelime,int aktifSayfa=1);
    }
}
