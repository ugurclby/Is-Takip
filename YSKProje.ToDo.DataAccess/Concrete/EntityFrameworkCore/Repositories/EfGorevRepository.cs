using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public Gorev RaporGetirGorevIdile(int gorevId)
        {
            using TodoContext context = new TodoContext();

            var sonuc = context.Gorev.Include(x => x.Raporlar).Include(x=>x.AppUser).Include(x=>x.Aciliyet).Where(x => x.Id == gorevId).FirstOrDefault();

            return sonuc;
        }
    }
}
