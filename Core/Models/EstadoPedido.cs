using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class EstadoPedido
{
    public byte EstadoPedidoId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<HistorialEstadoPedido> HistorialEstadoPedidoEstadoAnteriors { get; set; } = new List<HistorialEstadoPedido>();

    public virtual ICollection<HistorialEstadoPedido> HistorialEstadoPedidoEstadoNuevos { get; set; } = new List<HistorialEstadoPedido>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}

public static class EstadoPedidoExtension
{
    public static ModelBuilder EstadoPedidoMapping(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<EstadoPedido>();


        entity.HasKey(e => e.EstadoPedidoId).HasName("PK__EstadoPe__7C6452B27F60ED59");

        entity.ToTable("EstadoPedido");

        entity.HasIndex(e => e.Codigo, "UQ__EstadoPe__06370DAC023D5A04").IsUnique();

        entity.Property(e => e.Codigo)
            .HasMaxLength(30)
            .IsUnicode(false);
        entity.Property(e => e.Descripcion)
            .HasMaxLength(100)
            .IsUnicode(false);


        return modelBuilder;
    }
}