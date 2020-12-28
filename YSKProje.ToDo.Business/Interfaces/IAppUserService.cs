using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Interfaces
{
    public interface IAppUserService
    {
        List<AppUser> GetirAdminOlmayan();
        List<AppUser> GetirAdminOlmayan(out int toplamSayfa, string aranacakKelime, int aktifSayfa);
        List<GorevTamamlamisPersonel> EnCokGorevTamamlamisPersonel();
        public List<GorevTamamlamisPersonel> EnCokGorevdeCalisanPersonel();
    }
}
