using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfBildirimRepository : EfGenericRepository<Bildirim>, IBildirimDal
    {
        public List<Bildirim> OkunmamisBildirim(int AppUserId)
        {
            using var context = new TodoContext();

            var bildirimler = context.Bildirim.Include(x => x.AppUser).Where(x => x.AppUserId == AppUserId && !x.Durum).ToList();

            return bildirimler;
        }

        public int OkunmayanBildirimSayisi(int AppUserId)
        {
            using var context = new TodoContext();

            return context.Bildirim.Where(x => !x.Durum && x.AppUserId==AppUserId).Count();
             
        }
    }
}
