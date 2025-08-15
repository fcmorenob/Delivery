using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Tienda
{
    public int TiendaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public string? Departamento { get; set; }

    public string? Telefono { get; set; }

    public bool Activa { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}

public static class TiendaExtension
{
    public static ModelBuilder TiendaMapping(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Tienda>();

       
            entity.HasKey(e => e.TiendaId);

            entity.Property(e => e.Activa).HasDefaultValue(true);
            entity.Property(e => e.Ciudad)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Departamento)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false);
     

        return modelBuilder;
    }
}
