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
    public class EfAppUserRepository : EfGenericRepository<AppUser>, IAppUserDal
    {
        public List<GorevTamamlamisPersonel> EnCokGorevTamamlamisPersonel()
        {
            using var context = new TodoContext();

            //Join ile yapılmış hali 
            //return context.Users.Join(context.Gorev, u => u.Id, g => g.AppUserId, (u, g) => new
            //{
            //    Isım = u.Name,
            //    GorevId = g.Id,
            //    Durum = g.Durum
            //}).Where(x => x.Durum).GroupBy(x => x.Isım).OrderBy(x => x.Count()).Take(5).Select(x => new GorevTamamlamisPersonel { GorevSayisi = x.Count(), Isım = x.Key }).ToList(); 

            return context.Gorev.Include(x => x.AppUser).Where(x => x.Durum).GroupBy(x => x.AppUser.Name).OrderByDescending(x => x.Count()).Take(5).Select(x => new GorevTamamlamisPersonel { GorevSayisi = x.Count(), Isim = x.Key }).ToList(); 
        }

        public List<GorevTamamlamisPersonel> EnCokGorevdeCalisanPersonel()
        {
            using var context = new TodoContext();

            //Join ile yapılmış hali 
            //return context.Users.Join(context.Gorev, u => u.Id, g => g.AppUserId, (u, g) => new
            //{
            //    Isım = u.Name,
            //    GorevId = g.Id,
            //    Durum = g.Durum
            //}).Where(x => !x.Durum && x.AppUserId != null).GroupBy(x => x.Isım).OrderBy(x => x.Count()).Take(5).Select(x => new GorevTamamlamisPersonel { GorevSayisi = x.Count(), Isım = x.Key }).ToList(); 

            return context.Gorev.Include(x => x.AppUser).Where(x => !x.Durum && x.AppUserId != null).GroupBy(x => x.AppUser.Name).OrderByDescending(x => x.Count()).Take(5).Select(x => new GorevTamamlamisPersonel { GorevSayisi = x.Count(), Isim = x.Key }).ToList();
        }

        public List<AppUser> GetirAdminOlmayan()
        {
            /*
             * select * from AspNetUsers inner join AspNetUserRoles 
             * on AspNetUsers.id = AspNetUserRoles.UserId
             * inner join AspNetRoles 
             * on AspNetUserRoles.RoleId = AspNetRoles.Id where AspNetRoles.Name='Member'
             Bu sorguyu linq ile aşağıda yazacağız.
             */

            using var context = new TodoContext();
            return context.Users.Join(context.UserRoles, user => user.Id, userRole => userRole.UserId, (resultUser, resultUserRole) => new
            {
                user = resultUser,
                userRole = resultUserRole
            }).Join(context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id, (resultTable, resultRole) => new
            {
                user = resultTable.user,
                userRole = resultTable.userRole,
                roles = resultRole
            }).Where(I => I.roles.Name == "Member").Select(I => new AppUser()
            {
                Id = I.user.Id,
                Name = I.user.Name,
                Surname = I.user.Surname,
                Picture = I.user.Picture,
                Email = I.user.Email,
                UserName = I.user.UserName
            }).ToList();
        }

        public List<AppUser> GetirAdminOlmayan(out int toplamSayfa,string aranacakKelime, int aktifSayfa = 1)
        {
            using var context = new TodoContext();
            var result = context.Users.Join(context.UserRoles, user => user.Id, userRole => userRole.UserId, (resultUser, resultUserRole) => new
            {
                user = resultUser,
                userRole = resultUserRole
            }).Join(context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id, (resultTable, resultRole) => new
            {
                user = resultTable.user,
                userRole = resultTable.userRole,
                roles = resultRole
            }).Where(I => I.roles.Name == "Member").Select(I => new AppUser()
            {
                Id = I.user.Id,
                Name = I.user.Name,
                Surname = I.user.Surname,
                Picture = I.user.Picture,
                Email = I.user.Email,
                UserName = I.user.UserName
            });
            toplamSayfa = (int)Math.Ceiling((double)result.Count()/3);

            if (!string.IsNullOrWhiteSpace(aranacakKelime))
            {
                result = result.Where(x => x.Name.ToLower().Contains(aranacakKelime.ToLower()) || x.Surname.ToLower().Contains(aranacakKelime.ToLower()));
                toplamSayfa = (int)Math.Ceiling((double)result.Count() / 3);
            }
            
            result = result.Skip((aktifSayfa - 1) * 3).Take(3); // paging
            return result.ToList();
        } 
    }
}
