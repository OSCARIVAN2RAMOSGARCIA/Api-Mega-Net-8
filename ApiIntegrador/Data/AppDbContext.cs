using ApiIntegrador.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiIntegrador.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Colonia> Colonias { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Paquete> Paquetes { get; set; }
        public DbSet<PaqueteServicio> PaqueteServicios { get; set; }
        public DbSet<Suscriptor> Suscriptores { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Promocion> Promociones { get; set; }
        public DbSet<PromocionConfiguracion> PromocionConfiguraciones { get; set; }
        public DbSet<PromocionAplicada> PromocionesAplicadas { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Ciudad>().HasKey(c => c.IdCiudad);
    modelBuilder.Entity<Colonia>().HasKey(c => c.IdColonia);
    modelBuilder.Entity<Colonia>()
        .HasOne(c => c.Ciudad)
        .WithMany(ci => ci.Colonias)
        .HasForeignKey(c => c.IdCiudad)
        .HasConstraintName("FK_Colonia_Ciudad");

    modelBuilder.Entity<Suscriptor>().HasKey(s => s.IdSuscriptor);
    modelBuilder.Entity<Suscriptor>()
        .HasOne(s => s.Colonia)
        .WithMany(co => co.Suscriptores)
        .HasForeignKey(s => s.IdColonia)
        .HasConstraintName("FK_Suscriptor_Colonia");

    modelBuilder.Entity<Paquete>().HasKey(p => p.IdPaquete);

    modelBuilder.Entity<Servicio>().HasKey(s => s.IdServicio);

    modelBuilder.Entity<PaqueteServicio>()
        .HasKey(ps => new { ps.IdPaquete, ps.IdServicio });
    modelBuilder.Entity<PaqueteServicio>()
        .HasOne(ps => ps.Paquete)
        .WithMany(p => p.PaqueteServicios)
        .HasForeignKey(ps => ps.IdPaquete)
        .HasConstraintName("FK_PaqueteServicio_Paquete");
    modelBuilder.Entity<PaqueteServicio>()
        .HasOne(ps => ps.Servicio)
        .WithMany(s => s.PaqueteServicios)
        .HasForeignKey(ps => ps.IdServicio)
        .HasConstraintName("FK_PaqueteServicio_Servicio");

    modelBuilder.Entity<Contrato>().HasKey(c => c.IdContrato);
    modelBuilder.Entity<Contrato>()
        .HasOne(c => c.Suscriptor)
        .WithMany(s => s.Contratos)
        .HasForeignKey(c => c.IdSuscriptor)
        .HasConstraintName("FK_Contrato_Suscriptor");
    modelBuilder.Entity<Contrato>()
        .HasOne(c => c.Paquete)
        .WithMany(p => p.Contratos)
        .HasForeignKey(c => c.IdPaquete)
        .HasConstraintName("FK_Contrato_Paquete");

    modelBuilder.Entity<Promocion>().HasKey(p => p.IdPromocion);

    modelBuilder.Entity<PromocionConfiguracion>().HasKey(pc => pc.IdPromocionConfiguracion);
    modelBuilder.Entity<PromocionConfiguracion>()
        .HasOne(pc => pc.Promocion)
        .WithMany(p => p.Configuraciones)
        .HasForeignKey(pc => pc.IdPromocion)
        .HasConstraintName("FK_PromocionConfiguracion_Promocion");
    modelBuilder.Entity<PromocionConfiguracion>()
        .HasOne(pc => pc.Ciudad)
        .WithMany()
        .HasForeignKey(pc => pc.IdCiudad)
        .HasConstraintName("FK_PromocionConfiguracion_Ciudad");
    modelBuilder.Entity<PromocionConfiguracion>()
        .HasOne(pc => pc.Colonia)
        .WithMany()
        .HasForeignKey(pc => pc.IdColonia)
        .HasConstraintName("FK_PromocionConfiguracion_Colonia");
    modelBuilder.Entity<PromocionConfiguracion>()
        .HasOne(pc => pc.Paquete)
        .WithMany()
        .HasForeignKey(pc => pc.IdPaquete)
        .HasConstraintName("FK_PromocionConfiguracion_Paquete");

    modelBuilder.Entity<PromocionAplicada>().HasKey(pa => pa.IdPromocionAplicada);
    modelBuilder.Entity<PromocionAplicada>()
        .HasOne(pa => pa.Contrato)
        .WithMany(c => c.PromocionesAplicadas)
        .HasForeignKey(pa => pa.IdContrato)
        .HasConstraintName("FK_PromocionAplicada_Contrato");
    modelBuilder.Entity<PromocionAplicada>()
        .HasOne(pa => pa.Promocion)
        .WithMany(p => p.PromocionesAplicadas)
        .HasForeignKey(pa => pa.IdPromocion)
        .HasConstraintName("FK_PromocionAplicada_Promocion");
    modelBuilder.Entity<PromocionAplicada>()
        .HasOne(pa => pa.PromocionConfiguracion)
        .WithMany(pc => pc.PromocionesAplicadas)
        .HasForeignKey(pa => pa.IdPromocionConfiguracion)
        .HasConstraintName("FK_PromocionAplicada_PromocionConfiguracion");
}

    }
}