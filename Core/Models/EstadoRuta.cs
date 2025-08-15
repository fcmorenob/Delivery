using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class EstadoRuta
{
    public byte EstadoRutaId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<RutaEntrega> RutaEntregas { get; set; } = new List<RutaEntrega>();
}

public static class EstadoRutaExtension
{
    public static ModelBuilder EstadoRutaMapping(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<EstadoRuta>();

            entity.HasKey(e => e.EstadoRutaId).HasName("PK__EstadoRu__B834E3CB060DEAE8");

            entity.HasIndex(e => e.Codigo, "UQ__EstadoRu__06370DAC08EA5793").IsUnique();

            entity.Property(e => e.Codigo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);

        return modelBuilder;
    }
}
