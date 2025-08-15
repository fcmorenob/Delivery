using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class HistorialEstadoPedido
{
    public long HistorialId { get; set; }

    public long PedidoId { get; set; }

    public byte? EstadoAnteriorId { get; set; }

    public byte EstadoNuevoId { get; set; }

    public DateTime FechaCambioUtc { get; set; }

    public string? UsuarioCambio { get; set; }

    public virtual EstadoPedido? EstadoAnterior { get; set; }

    public virtual EstadoPedido EstadoNuevo { get; set; } = null!;

    public virtual Pedido Pedido { get; set; } = null!;
}

public static class HistorialEstadoPedidoExtension
{
    public static ModelBuilder HistorialEstadoPedidoMapping(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<HistorialEstadoPedido>();
        
            entity.HasKey(e => e.HistorialId);

            entity.ToTable("HistorialEstadoPedido");

            entity.HasIndex(e => new { e.PedidoId, e.FechaCambioUtc }, "IX_Historial_Pedido_Fecha").IsDescending(false, true);

            entity.Property(e => e.FechaCambioUtc)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.UsuarioCambio)
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.HasOne(d => d.EstadoAnterior).WithMany(p => p.HistorialEstadoPedidoEstadoAnteriors)
                .HasForeignKey(d => d.EstadoAnteriorId)
                .HasConstraintName("FK_Historial_EstadoAnterior");

            entity.HasOne(d => d.EstadoNuevo).WithMany(p => p.HistorialEstadoPedidoEstadoNuevos)
                .HasForeignKey(d => d.EstadoNuevoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_EstadoNuevo");

            entity.HasOne(d => d.Pedido).WithMany(p => p.HistorialEstadoPedidos)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Pedido");

        return modelBuilder;
    }
}
