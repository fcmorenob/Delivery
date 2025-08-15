using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class RutaEntrega
{
    public long RutaId { get; set; }

    public DateOnly Fecha { get; set; }

    public string? Zona { get; set; }

    public int VehiculoId { get; set; }

    public int ConductorId { get; set; }

    public byte EstadoRutaId { get; set; }

    public DateTime? HoraInicioUtc { get; set; }

    public DateTime? HoraFinUtc { get; set; }

    public DateTime CreatedUtc { get; set; }

    public virtual Conductor Conductor { get; set; } = null!;

    public virtual EstadoRuta EstadoRuta { get; set; } = null!;

    public virtual ICollection<ParadaEntrega> ParadaEntregas { get; set; } = new List<ParadaEntrega>();

    public virtual Vehiculo Vehiculo { get; set; } = null!;
}

public static class RutaEntregaExtension
{
    public static ModelBuilder RutaEntregaMapping(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<RutaEntrega>();

        modelBuilder.Entity<RutaEntrega>(entity =>
        {
            entity.HasKey(e => e.RutaId);

            entity.ToTable("RutaEntrega");

            entity.HasIndex(e => new { e.EstadoRutaId, e.Fecha }, "IX_RutaEntrega_Estado");

            entity.HasIndex(e => new { e.Fecha, e.Zona }, "IX_RutaEntrega_Fecha_Zona");

            entity.Property(e => e.CreatedUtc)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.HoraFinUtc).HasPrecision(3);
            entity.Property(e => e.HoraInicioUtc).HasPrecision(3);
            entity.Property(e => e.Zona)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.Conductor).WithMany(p => p.RutaEntregas)
                .HasForeignKey(d => d.ConductorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RutaEntrega_Conductor");

            entity.HasOne(d => d.EstadoRuta).WithMany(p => p.RutaEntregas)
                .HasForeignKey(d => d.EstadoRutaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RutaEntrega_Estado");

            entity.HasOne(d => d.Vehiculo).WithMany(p => p.RutaEntregas)
                .HasForeignKey(d => d.VehiculoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RutaEntrega_Vehiculo");
        });

        return modelBuilder;
    }
}
