﻿using Microsoft.EntityFrameworkCore;
using ReporteTributario.Models.Entities;


namespace ReporteTributario.Models
{
    public class DbTtributarioContext : DbContext
    {
        public DbTtributarioContext()
        {

        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<InformacionBase> InformacionBase { get; set; }

        public DbTtributarioContext(DbContextOptions<DbTtributarioContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Usuario>(usuario =>
            {
                usuario.ToTable("USUARIO");
                usuario.HasKey(p => p.IdUsuario);

                //tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);

                usuario.Property(p => p.NombreUsuario).IsRequired().HasMaxLength(200);

                usuario.Property(p => p.Correo).IsRequired().HasMaxLength(150);

                usuario.Property(p => p.Clave).IsRequired().HasMaxLength(200);

                usuario.Property(p => p.Restablecer);

                usuario.Property(p => p.Confirmado);

                usuario.Property(p => p.Token);

            });

            modelBuilder.Entity<InformacionBase>(usuario =>
            {
                usuario.ToTable("INFORMACION_BASE");
                usuario.HasKey(p => p.IdImpuesto);

                //tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);

                usuario.Property(p => p.Impuesto).IsRequired().HasMaxLength(200);

                usuario.Property(p => p.Ciudad).IsRequired().HasMaxLength(150);

                usuario.Property(p => p.Departamento).IsRequired().HasMaxLength(200);

                usuario.Property(p => p.FechaLimite);

                usuario.Property(p => p.Responsable);

                usuario.Property(p => p.Periodo);

                usuario.Property(p => p.Periodicidad);

            });

        }
    }
}
