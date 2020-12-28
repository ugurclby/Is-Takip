using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Interfaces
{
    public interface IRaporDal : IGenericDal<Rapor>
    {
        Rapor RaporGetirGorevileId(int RaporId);
        int GetirRaporSayisiileAppUserId(int id);
        int ToplamYazilanRaporSayisi();
    }
}
