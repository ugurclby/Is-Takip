﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfRaporRepository : EfGenericRepository<Rapor>, IRaporDal
    {
        public Rapor RaporGetirGorevileId(int RaporId)
        {
            using TodoContext context = new TodoContext();

            var rapor =  context.Rapor.Include(x => x.Gorev).ThenInclude(x=>x.Aciliyet).Where(x => x.Id == RaporId).FirstOrDefault();

            return rapor;

        }
        public int GetirRaporSayisiileAppUserId(int id)
        {
            using var context = new TodoContext();
            var result = context.Gorev.Include(I => I.Raporlar).Where(I => I.AppUserId == id);

            return result.SelectMany(I => I.Raporlar).Count();
        }

        public int ToplamYazilanRaporSayisi()
        {
            using TodoContext context = new TodoContext();

            return context.Rapor.Count();

        }
    }
}
