﻿using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Interfaces
{
    public interface IBildirimService:IGenericService<Bildirim>
    {
        List<Bildirim> OkunmamisBildirim(int AppUserId);
        int OkunmayanBildirimSayisi(int AppUserId);
    }
}
