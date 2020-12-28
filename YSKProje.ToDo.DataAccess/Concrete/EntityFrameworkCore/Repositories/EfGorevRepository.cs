using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGorevRepository : EfGenericRepository<Gorev>, IGorevDal
    {
        public Gorev GetirAciliyetileId(int id)
        {
            using TodoContext context = new TodoContext();
            return context.Gorev.Include(x => x.Aciliyet).FirstOrDefault(x => !x.Durum && x.Id == id);
        }

        public List<Gorev> GetirAciliyetİleTamamlanmayan()
        {
            using (TodoContext context = new TodoContext())
            {
                return context.Gorev.Include(x => x.Aciliyet).Where(x => !x.Durum).OrderByDescending(x => x.OlusturulmaTarih).ToList();
            }
        }

        public List<Gorev> GetirileAppUserId(int userId)
        {
            using (TodoContext context = new TodoContext())
            {
                return context.Gorev.Where(x=> x.AppUserId== userId).ToList();
            }
        }

        public List<Gorev> GetirTumTablolarla()
        {
            using TodoContext context = new TodoContext();
            return context.Gorev.Include(x => x.Aciliyet).Include(x => x.AppUser).Include(x => x.Raporlar).Where(x => !x.Durum).OrderByDescending(x => x.OlusturulmaTarih).ToList();
        }

        public List<Gorev> GetirTumTablolarla(Expression<Func<Gorev, bool>> filter)
        {
            using TodoContext context = new TodoContext();
            return context.Gorev.Include(x => x.Aciliyet).Include(x => x.AppUser).Include(x => x.Raporlar).Where(filter).OrderByDescending(x => x.OlusturulmaTarih).ToList();
        }

        public List<Gorev> GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, int userId, int aktifSayfa = 1)
        {
            using TodoContext context = new TodoContext();
            var gorev = context.Gorev.Include(x => x.Aciliyet).Include(x => x.AppUser).Include(x => x.Raporlar).Where(x => x.AppUserId == userId && x.Durum).OrderByDescending(x => x.OlusturulmaTarih);
            
            toplamSayfa = (int) Math.Ceiling((double)gorev.Count()/3);

            return gorev.Skip((aktifSayfa - 1) * 3).Take(3).ToList();
        }

        public Gorev RaporGetirGorevIdile(int gorevId)
        {
            using TodoContext context = new TodoContext();

            var sonuc = context.Gorev.Include(x => x.Raporlar).Include(x=>x.AppUser).Include(x=>x.Aciliyet).Where(x => x.Id == gorevId).FirstOrDefault();

            return sonuc;
        }

        public int TamamlananGorevSayisi(int AppUserId)
        {
            using TodoContext context = new TodoContext();

            return context.Gorev.Where(x => x.Durum && x.AppUserId==AppUserId).Count();
        }

        public int TamamlanmasiGerekenGorevSayisi(int AppUserId)
        {
            using TodoContext context = new TodoContext();

            return context.Gorev.Where(x => !x.Durum && x.AppUserId == AppUserId).Count();
        }
    }
}
