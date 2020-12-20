using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Concrete
{
    public class GorevManager : IGorevService
    {
        private readonly IGorevDal _gorevDal ;

        public GorevManager(IGorevDal gorevDal)
        {
            _gorevDal = gorevDal;
        }

        public Gorev GetirAciliyetileId(int id)
        {
            return _gorevDal.GetirAciliyetileId(id);
        }

        public List<Gorev> GetirAciliyetİleTamamlanmayan()
        {
            return _gorevDal.GetirAciliyetİleTamamlanmayan();
        }

        public List<Gorev> GetirHepsi()
        {
            return _gorevDal.GetirHepsi();
        }

        public Gorev GetirIdile(int id)
        {
            return _gorevDal.GetirIdile(id);
        }

        public List<Gorev> GetirileAppUserId(int userId)
        {
            return _gorevDal.GetirileAppUserId(userId);
        }

        public List<Gorev> GetirTumTablolarla()
        {
            return _gorevDal.GetirTumTablolarla();
        }

        public List<Gorev> GetirTumTablolarla(Expression<Func<Gorev, bool>> filter)
        {
            return _gorevDal.GetirTumTablolarla(filter);
        }

        public List<Gorev> GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, int userId, int aktifSayfa = 1)
        {
            return _gorevDal.GetirTumTablolarlaTamamlanmayan(out toplamSayfa, userId, aktifSayfa);
        }

        public void Guncelle(Gorev tablo)
        {
            _gorevDal.Guncelle(tablo);
        }

        public void Kaydet(Gorev tablo)
        {
            _gorevDal.Kaydet(tablo);
        }

        public Gorev RaporGetirGorevIdile(int gorevId)
        {
            return _gorevDal.RaporGetirGorevIdile(gorevId);
        }

        public void Sil(Gorev tablo)
        {
            _gorevDal.Sil(tablo);
        }

    }
}
