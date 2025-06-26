using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiIntegrador.Models;

namespace ApiIntegrador.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Colonia> Colonias { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Paquete> Paquetes { get; set; }
        public DbSet<PaqueteServicio> PaqueteServicios { get; set; }
        public DbSet<Suscriptor> Suscriptores { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Promocion> Promociones { get; set; }
        public DbSet<PromocionAplicada> PromocionesAplicadas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Claves primarias
            modelBuilder.Entity<Ciudad>().HasKey(c => c.IdCiudad);
            modelBuilder.Entity<Colonia>().HasKey(c => c.IdColonia);
            modelBuilder.Entity<Servicio>().HasKey(s => s.IdServicio);
            modelBuilder.Entity<Paquete>().HasKey(p => p.IdPaquete);
            modelBuilder.Entity<PaqueteServicio>().HasKey(ps => new { ps.IdPaquete, ps.IdServicio });
            modelBuilder.Entity<Suscriptor>().HasKey(su => su.IdSuscriptor);
            modelBuilder.Entity<Contrato>().HasKey(c => c.IdContrato);
            modelBuilder.Entity<Promocion>().HasKey(p => p.IdPromocion);
            modelBuilder.Entity<PromocionAplicada>().HasKey(pa => pa.IdPromocionAplicada);

            // Precisión para decimales
            modelBuilder.Entity<Promocion>(entity =>
            {
                entity.Property(p => p.DescuentoEmpresarial).HasPrecision(10, 2);
                entity.Property(p => p.DescuentoResidencial).HasPrecision(10, 2);
            });

            modelBuilder.Entity<PromocionAplicada>(entity =>
            {
                entity.Property(pa => pa.DescuentoAplicado).HasPrecision(10, 2);
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.Property(s => s.PrecioEmpresarial).HasPrecision(10, 2);
                entity.Property(s => s.PrecioResidencial).HasPrecision(10, 2);
            });

            // Relaciones
            modelBuilder.Entity<Colonia>()
                .HasOne(c => c.Ciudad)
                .WithMany(ci => ci.Colonias)
                .HasForeignKey(c => c.IdCiudad)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Suscriptor>()
                .HasOne(su => su.Colonia)
                .WithMany(co => co.Suscriptores)
                .HasForeignKey(su => su.IdColonia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contrato>()
                .HasOne(c => c.Suscriptor)
                .WithMany(su => su.Contratos)
                .HasForeignKey(c => c.IdSuscriptor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contrato>()
                .HasOne(c => c.Paquete)
                .WithMany(p => p.Contratos)
                .HasForeignKey(c => c.IdPaquete)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PaqueteServicio>()
                .HasOne(ps => ps.Paquete)
                .WithMany(p => p.PaqueteServicios)
                .HasForeignKey(ps => ps.IdPaquete);

            modelBuilder.Entity<PaqueteServicio>()
                .HasOne(ps => ps.Servicio)
                .WithMany(s => s.PaqueteServicios)
                .HasForeignKey(ps => ps.IdServicio);

            modelBuilder.Entity<PromocionAplicada>()
                .HasOne(pa => pa.Contrato)
                .WithMany(c => c.PromocionesAplicadas)
                .HasForeignKey(pa => pa.IdContrato);

            modelBuilder.Entity<PromocionAplicada>()
                .HasOne(pa => pa.Promocion)
                .WithMany(p => p.PromocionesAplicadas)
                .HasForeignKey(pa => pa.IdPromocion);

            // Índices
            modelBuilder.Entity<Ciudad>().HasIndex(c => c.Nombre).IsUnique();
            modelBuilder.Entity<Colonia>().HasIndex(c => new { c.IdCiudad, c.Nombre }).IsUnique();
            modelBuilder.Entity<Servicio>().HasIndex(s => s.Nombre).IsUnique();
            modelBuilder.Entity<Paquete>().HasIndex(p => p.Nombre).IsUnique();
            modelBuilder.Entity<Suscriptor>().HasIndex(su => su.Nombre);
        }

        // Método para verificar la conexión a la base de datos
        public async Task<bool> IsDatabaseConnectedAsync()
        {
            try
            {
                return await this.Database.CanConnectAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
