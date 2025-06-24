using Microsoft.EntityFrameworkCore;
using ApiIntegrador.Models;

namespace ApiIntegrador.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Paquete> Paquetes { get; set; }
        public DbSet<PaqueteServicio> PaqueteServicios { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Colonia> Colonias { get; set; }
        public DbSet<Suscriptor> Suscriptores { get; set; }
        public DbSet<Promocion> Promociones { get; set; }
        public DbSet<PromocionAplicada> PromocionesAplicadas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaqueteServicio>()
                .HasKey(ps => new { ps.IdPaquete, ps.IdServicio });

            modelBuilder.Entity<PaqueteServicio>()
                .HasOne(ps => ps.Paquete)
                .WithMany(p => p.PaqueteServicios)
                .HasForeignKey(ps => ps.IdPaquete);

            modelBuilder.Entity<PaqueteServicio>()
                .HasOne(ps => ps.Servicio)
                .WithMany(s => s.PaqueteServicios)
                .HasForeignKey(ps => ps.IdServicio);

            modelBuilder.Entity<Ciudad>()
                .HasKey(c => c.IdCiudad);

            modelBuilder.Entity<Colonia>()
                .HasKey(c => c.IdColonia);

            modelBuilder.Entity<Paquete>()
                .HasKey(c => c.IdPaquete);

            modelBuilder.Entity<Promocion>()
                .HasKey(c => c.IdPromocion);

            modelBuilder.Entity<PromocionAplicada>()
                .HasKey(c => c.IdAplicacion);

            modelBuilder.Entity<Servicio>()
                .HasKey(c => c.IdServicio);

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.Property(e => e.PrecioContratacion)
                    .HasPrecision(10, 2);

                entity.Property(e => e.PrecioMensual)
                    .HasPrecision(10, 2);
            });

            modelBuilder.Entity<Suscriptor>()
                .HasKey(c => c.IdSuscriptor);
        }
    }
}
