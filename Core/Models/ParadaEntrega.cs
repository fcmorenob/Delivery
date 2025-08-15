using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class ParadaEntrega
{
    public long ParadaId { get; set; }

    public long RutaId { get; set; }

    public long PedidoId { get; set; }

    public int OrdenSecuencia { get; set; }

    public DateTime? Etautc { get; set; }

    public DateTime? HoraRealUtc { get; set; }

    public byte EstadoParadaId { get; set; }

    public byte IntentosEntrega { get; set; }

    public string? PodTipo { get; set; }

    public string? PodDato { get; set; }

    public virtual EstadoParada EstadoParada { get; set; } = null!;

    public virtual Pedido Pedido { get; set; } = null!;

    public virtual RutaEntrega Ruta { get; set; } = null!;
}

public static class ParadaEntregaExtension
{
    public static ModelBuilder ParadaEntregaMapping(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<ParadaEntrega>();

           entity.HasKey(e => e.ParadaId);

            entity.ToTable("ParadaEntrega");

            entity.HasIndex(e => e.EstadoParadaId, "IX_Parada_Estado").HasFilter("([EstadoParadaId]<>(1))");

            entity.HasIndex(e => e.PedidoId, "IX_Parada_Pedido");

            entity.HasIndex(e => new { e.RutaId, e.OrdenSecuencia }, "UX_Parada_Ruta_Orden").IsUnique();

            entity.Property(e => e.Etautc)
                .HasPrecision(3)
                .HasColumnName("ETAUtc");
            entity.Property(e => e.HoraRealUtc).HasPrecision(3);
            entity.Property(e => e.PodDato)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PodTipo)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.EstadoParada).WithMany(p => p.ParadaEntregas)
                .HasForeignKey(d => d.EstadoParadaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParadaEntrega_Estado");

            entity.HasOne(d => d.Pedido).WithMany(p => p.ParadaEntregas)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParadaEntrega_Pedido");

            entity.HasOne(d => d.Ruta).WithMany(p => p.ParadaEntregas)
                .HasForeignKey(d => d.RutaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParadaEntrega_Ruta");

        return modelBuilder;
    }
}
