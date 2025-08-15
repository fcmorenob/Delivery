using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Pedido
{
    public long PedidoId { get; set; }

    public long ClienteId { get; set; }

    public int TiendaId { get; set; }

    public byte EstadoPedidoId { get; set; }

    public decimal Total { get; set; }

    public DateTime? VentanaEntregaInicio { get; set; }

    public DateTime? VentanaEntregaFin { get; set; }

    public string? DireccionEntrega { get; set; }

    public decimal? GeoLat { get; set; }

    public decimal? GeoLng { get; set; }

    public string? CanalOrigen { get; set; }

    public DateTime FechaCreacionUtc { get; set; }

    public DateTime FechaActualizacionUtc { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual EstadoPedido EstadoPedido { get; set; } = null!;

    public virtual ICollection<HistorialEstadoPedido> HistorialEstadoPedidos { get; set; } = new List<HistorialEstadoPedido>();

    public virtual ICollection<ParadaEntrega> ParadaEntregas { get; set; } = new List<ParadaEntrega>();

    public virtual ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();

    public virtual Tiendum Tienda { get; set; } = null!;
}

public static class PedidoExtension
{
    public static ModelBuilder PedidoMapping(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Pedido>();

        
            entity.ToTable("Pedido");

            entity.HasIndex(e => e.ClienteId, "IX_Pedido_Cliente");

            entity.HasIndex(e => e.FechaCreacionUtc, "IX_Pedido_FechaCreacion");

            entity.HasIndex(e => new { e.TiendaId, e.EstadoPedidoId, e.FechaCreacionUtc }, "IX_Pedido_Tienda_Estado").IsDescending(false, false, true);

            entity.Property(e => e.CanalOrigen)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.DireccionEntrega)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaActualizacionUtc)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.FechaCreacionUtc)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.GeoLat).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.GeoLng).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.VentanaEntregaFin).HasPrecision(3);
            entity.Property(e => e.VentanaEntregaInicio).HasPrecision(3);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Cliente");

            entity.HasOne(d => d.EstadoPedido).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.EstadoPedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Estado");

            entity.HasOne(d => d.Tienda).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.TiendaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Tienda");
     

        return modelBuilder;
    }
}