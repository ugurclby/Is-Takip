using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Interfaces
{
    public interface IBildirimDal:IGenericDal<Bildirim>
    {
        List<Bildirim> OkunmamisBildirim(int AppUserId);
        int OkunmayanBildirimSayisi(int AppUserId);
    }
}
