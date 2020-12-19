using Ciudadano.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ciudadano.DataContext
{
    public class CiudadanoDataContext : DbContext
    {
        public DbSet<CiudadanoI> Ciudadanos { get; set; }
        public DbSet<EstadoCivil> EstadosCiviles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;DataBase=CiudadanoDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CiudadanoMap());
            modelBuilder.ApplyConfiguration(new EstadoCivilMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
