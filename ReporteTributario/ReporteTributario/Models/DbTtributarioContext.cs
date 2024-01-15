using Microsoft.EntityFrameworkCore;
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


            modelBuilder.Entity<Usuario>(model =>
            {
                model.ToTable("USUARIO");
                model.HasKey(p => p.IdUsuario);

                //tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);

                model.Property(p => p.NombreUsuario).IsRequired().HasMaxLength(200);

                model.Property(p => p.Correo).IsRequired().HasMaxLength(150);

                model.Property(p => p.Clave).IsRequired().HasMaxLength(200);

                model.Property(p => p.Restablecer);

                model.Property(p => p.Confirmado);

                model.Property(p => p.Token);

            });

            modelBuilder.Entity<InformacionBase>(model =>
            {
                model.ToTable("INFORMACION_BASE");
                model.HasKey(p => p.IdImpuesto);

                //tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);

                model.Property(p => p.Impuesto).IsRequired().HasMaxLength(200);

                model.Property(p => p.Ciudad).IsRequired().HasMaxLength(150);

                model.Property(p => p.Departamento).IsRequired().HasMaxLength(200);

                model.Property(p => p.FechaLimite);

                model.Property(p => p.Responsable);

                model.Property(p => p.Periodo);

                model.Property(p => p.Periodicidad);

            });

        }
    }
}
