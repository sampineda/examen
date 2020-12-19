using Ciudadano.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ciudadano.DataContext
{
    public class CiudadanoMap : IEntityTypeConfiguration<CiudadanoI>
    {
            public void Configure(EntityTypeBuilder<CiudadanoI> builder)
            {
                builder.ToTable("Ciudadanos", "dbo");
                builder.HasKey(q => q.Id);
                builder.Property(e => e.Id).IsRequired().UseSqlServerIdentityColumn();
                builder.Property(e => e.Nombre).HasColumnType("varchar(50)")
                    .HasMaxLength(50).IsRequired();

                builder.HasOne(e => e.EstadoCivil)
                    .WithMany(e => e.Ciudadanos)
                    .HasForeignKey(e => e.EstadoCivilId);
            }
    }

    public class EstadoCivilMap : IEntityTypeConfiguration<EstadoCivil>
    {
        public void Configure(EntityTypeBuilder<EstadoCivil> builder)
        {
            builder.ToTable("EstadosCiviles", "dbo");
            builder.HasKey(q => q.Id);
            builder.Property(e => e.Id).IsRequired().UseSqlServerIdentityColumn();
            builder.Property(e => e.Estado).HasColumnType("varchar(50)")
                .HasMaxLength(50).IsRequired();
        }
    }

    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios", "dbo");
            builder.HasKey(q => q.UsuarioId);
            builder.Property(e => e.UsuarioId).IsRequired();
            builder.Property(e => e.Contrasenia).HasColumnType("varchar(50)")
                .HasMaxLength(50).IsRequired();
            builder.Property(e => e.EstaActivo);

        }
    }
}
