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
        public List<Gorev> GetirAciliyetİleTamamlanmayan()
        {
            using(var context = new TodoContext())
            {
                return context.Gorev.Include(x => x.Aciliyet).Where(x => !x.Durum).OrderByDescending(x => x.OlusturulmaTarih).ToList();
            }
        }
    }
}
