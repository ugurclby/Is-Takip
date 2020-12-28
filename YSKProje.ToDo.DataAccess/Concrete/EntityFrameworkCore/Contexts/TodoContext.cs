using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts
{
    public class TodoContext : IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb; database=udemyBlogToDo; Trusted_Connection=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.ApplyConfiguration(new GorevMap());
            modelBuilder.ApplyConfiguration(new AciliyetMap());
            modelBuilder.ApplyConfiguration(new RaporMap());
            modelBuilder.ApplyConfiguration(new AppUserMap());
            modelBuilder.ApplyConfiguration(new BildirimMap());

            base.OnModelCreating(modelBuilder);
        }

      
        public DbSet<Gorev> Gorev { get; set; }
        public DbSet<Aciliyet> Aciliyet  { get; set; } 
        public DbSet<Rapor> Rapor { get; set; }
        public DbSet<Bildirim> Bildirim { get; set; }
    }
}
