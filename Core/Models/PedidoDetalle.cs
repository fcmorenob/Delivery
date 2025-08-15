using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class PedidoDetalle
{
    public long PedidoDetalleId { get; set; }

    public long PedidoId { get; set; }

    public string NombreProducto { get; set; } = null!;

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual Pedido Pedido { get; set; } = null!;
}

public static class PedidoDetalleExtension
{
    public static ModelBuilder PedidoDetalleMapping(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<PedidoDetalle>();

            entity.ToTable("PedidoDetalle");

            entity.HasIndex(e => e.PedidoId, "IX_PedidoDetalle_Pedido");

            entity.Property(e => e.NombreProducto)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([Cantidad]*[PrecioUnitario])", true)
                .HasColumnType("decimal(29, 2)");

            entity.HasOne(d => d.Pedido).WithMany(p => p.PedidoDetalles)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PedidoDetalle_Pedido");

        return modelBuilder;
    }
}