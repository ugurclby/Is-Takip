using System.Collections.Generic;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Interfaces
{
    public interface IGorevDal : IGenericDal<Gorev>
    {
        List<Gorev> GetirAciliyetİleTamamlanmayan();
        List<Gorev> GetirTumTablolarla(); 
        Gorev GetirAciliyetileId(int id);
        List<Gorev> GetirileAppUserId(int userId);
        Gorev RaporGetirGorevIdile(int gorevId);
    }
}
