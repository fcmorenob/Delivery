using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class EstadoParada
{
    public byte EstadoParadaId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<ParadaEntrega> ParadaEntregas { get; set; } = new List<ParadaEntrega>();
}

public static class EstadoParadaExtension

{
    public static ModelBuilder EstadoParadaMapping(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<EstadoParada>();

        
            entity.HasKey(e => e.EstadoParadaId).HasName("PK__EstadoPa__E6BB84DA0CBAE877");

            entity.HasIndex(e => e.Codigo, "UQ__EstadoPa__06370DAC0F975522").IsUnique();

            entity.Property(e => e.Codigo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        
        return modelBuilder;
    }
}

